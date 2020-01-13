#define IS_BETA

using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.ViewModels;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.Logging;
using Markdig;
using ProjectCeleste.GameFiles.GameScanner.FileDownloader;
using Serilog;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Celeste_Launcher_Gui.Services
{
    public static class UpdateService
    {
#if IS_BETA

        private const string AssemblyInfoUrl =
            "https://raw.githubusercontent.com/ProjectCeleste/Celeste_Launcher/beta/Celeste_Launcher_Gui/Celeste_Launcher_Gui.csproj";

        private const string ChangelogUrl =
            "https://raw.githubusercontent.com/ProjectCeleste/Celeste_Launcher/beta/CHANGELOG.md";

#else

        private const string AssemblyInfoUrl =
            "https://raw.githubusercontent.com/ProjectCeleste/Celeste_Launcher/beta/Celeste_Launcher_Gui/Celeste_Launcher_Gui.csproj";

        private const string ChangelogUrl =
            "https://raw.githubusercontent.com/ProjectCeleste/Celeste_Launcher/master/CHANGELOG.md";

#endif

        private const string ReleaseZipUrl =
            "https://github.com/ProjectCeleste/Celeste_Launcher/releases/download/";

        private const string ZipName = "Celeste_Launcher.zip";

        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public static async Task<Version> GetGitHubAssemblyVersion()
        {
            string version;

            using (var client = new HttpClient())
            {
                var responseContent = await client.GetAsync(AssemblyInfoUrl).ConfigureAwait(false);
                version = await responseContent.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            var regex = new Regex(@"\<Version\>(\d{1,})\.(\d{1,})\.(\d{1,})<\/Version\>");
            var match = regex.Match(version);

            if (!match.Success)
                throw new Exception("GetLatestVersion() match.Success != true");

            return new Version($"{match.Groups[1]}.{match.Groups[2]}.{match.Groups[3]}");
        }

        public static async Task<string> GetChangeLog()
        {
            try
            {
                string changelogRaw;

                using (var client = new HttpClient())
                {
                    var responseContent = await client.GetAsync(ChangelogUrl).ConfigureAwait(false);
                    changelogRaw = await responseContent.Content.ReadAsStringAsync().ConfigureAwait(false);
                }

                if (string.IsNullOrWhiteSpace(changelogRaw))
                    return Properties.Resources.UpdateServiceChangelogError;

                var changelogFormatted = StripHtml(Markdown.ToHtml(changelogRaw))
                    .Replace("Full Changelog", string.Empty).Replace("Change Log", string.Empty);

                if (!string.IsNullOrWhiteSpace(changelogFormatted))
                    return changelogFormatted;

                return Properties.Resources.UpdateServiceChangelogError;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return Properties.Resources.UpdateServiceChangelogError;
            }
        }

        public static async Task LoadUpdateInfo(LauncherVersionInfo launcherVersionInfo)
        {
            launcherVersionInfo.CurrentVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
            launcherVersionInfo.NewVersion = (await GetGitHubAssemblyVersion()).ToString();
            launcherVersionInfo.ChangeLog = await GetChangeLog();
        }

        private static string StripHtml(string htmlText, bool decode = true)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(htmlText, "");
            return decode ? WebUtility.HtmlDecode(stripped) : stripped;
        }

        public static async Task DownloadAndInstallUpdate(bool isSteam = false, IProgress<int> progress = null,
            CancellationToken ct = default(CancellationToken))
        {
            Misc.CleanUpFiles(Directory.GetCurrentDirectory(), "*.old");

            var gitVersion = await GetGitHubAssemblyVersion().ConfigureAwait(false);

            progress?.Report(3);

            if (gitVersion <= Assembly.GetExecutingAssembly().GetName().Version)
                return;

            ct.ThrowIfCancellationRequested();

#if IS_BETA
            var downloadLink = $"{ReleaseZipUrl}v{gitVersion.Major}.{gitVersion.Minor}.{gitVersion.Build}-beta/{ZipName}";
#else
            var downloadLink = $"{ReleaseZipUrl}v{gitVersion.Major}.{gitVersion.Minor}.{gitVersion.Build}/{ZipName}";
#endif

            //Download File
            progress?.Report(5);

            var tempFileName = Path.GetTempFileName();

            try
            {
                var downloadFileAsync = new SimpleFileDownloader(downloadLink, tempFileName);
                if (progress != null)
                    downloadFileAsync.ProgressChanged += (sender, args) =>
                    {
                        progress.Report(Convert.ToInt32(Math.Floor(70 * (downloadFileAsync.DownloadProgress / 100))));
                    };
                await downloadFileAsync.DownloadAsync(ct);
            }
            catch (AggregateException)
            {
                if (File.Exists(tempFileName))
                    File.Delete(tempFileName);

                throw;
            }

            //Extract File
            progress?.Report(65);

            Progress<double> extractProgress = null;
            if (progress != null)
            {
                extractProgress = new Progress<double>();
                extractProgress.ProgressChanged += (o, ea) =>
                {
                    progress.Report(70 + Convert.ToInt32(Math.Floor(20 * (ea / 100))));
                };
            }
            var tempDir = Path.Combine(Path.GetTempPath(), $"Celeste_Launcher_v{gitVersion}");

            if (Directory.Exists(tempDir))
                Misc.CleanUpFiles(tempDir, "*.*");

            try
            {
                await ZipUtils.ExtractZipFile(tempFileName, tempDir, extractProgress, ct);
            }
            catch (AggregateException)
            {
                Misc.CleanUpFiles(tempDir, "*.*");
                throw;
            }
            finally
            {
                if (File.Exists(tempFileName))
                    File.Delete(tempFileName);
            }

            //Move File
            progress?.Report(90);

            var destinationDir = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
            try
            {
                Misc.MoveFiles(tempDir, destinationDir);
            }
            finally
            {
                Misc.CleanUpFiles(tempDir, "*.*");
            }

            //isSteam Version
            if (isSteam)
            {
                progress?.Report(95);
                Steam.ConvertToSteam(destinationDir);
            }

            //Clean Old File
            progress?.Report(97);
            Misc.CleanUpFiles(Directory.GetCurrentDirectory(), "*.old");

            //
            progress?.Report(100);
        }
    }
}