#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.Helpers;
using Markdig;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class UpdaterForm : Form
    {
        private const string AssemblyInfoUrl =
                "https://raw.githubusercontent.com/ProjectCeleste/Celeste_Launcher/dev_v2/Celeste_Launcher_Gui/Properties/AssemblyInfo.cs"
            ;

        private const string ChangelogUrl =
            "https://raw.githubusercontent.com/ProjectCeleste/Celeste_Launcher/dev_v2/CHANGELOG.md";

        private const string ReleaseZipUrl =
            "https://github.com/ProjectCeleste/Celeste_Launcher/releases/download/v";

        private CancellationTokenSource _cts = new CancellationTokenSource();

        public UpdaterForm()
        {
            InitializeComponent();
            
            SkinHelper.SetFont(Controls);

            richTextBox1.SetInnerMargins(20, 15, 20, 15);
        }

        private static string StripHtml(string htmlText, bool decode = true)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(htmlText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }

        private async void UpdaterForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(10, 12, 10, 12));
            }
            catch (Exception)
            {
                //
            }

            lbl_CurrentV.Text = $@"Current Version: v{Assembly.GetEntryAssembly().GetName().Version}";

            var gitVersion = await GetGitHubVersion();
            lbl_LatestV.Text = $@"Latest Version: v{gitVersion}";

            richTextBox1.Text = await GetChangeLog();

            if (gitVersion > Assembly.GetExecutingAssembly().GetName().Version)
                return;

            btnSmall1.Enabled = false;
        }

        public static async Task<Version> GetGitHubVersion()
        {
            string version;

            using (var client = new HttpClient())
            {
                var responseContent = await client.GetAsync(AssemblyInfoUrl).ConfigureAwait(false);
                version = await responseContent.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            var regex = new Regex(@"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]");
            var match = regex.Match(version);

            if (!match.Success)
                throw new Exception("GetLatestVersion() match.Success != true");

            var gitVersion = new Version($"{match.Groups[1]}.{match.Groups[2]}.{match.Groups[3]}.{match.Groups[4]}");

            return gitVersion;
        }
        
        private static async Task<string> GetChangeLog()
        {
            try
            {
                string changelogRaw;

                using (var client = new HttpClient())
                {
                    var responseContent = await client.GetAsync(ChangelogUrl).ConfigureAwait(false);
                    changelogRaw = await responseContent.Content.ReadAsStringAsync().ConfigureAwait(false);
                }

                if (string.IsNullOrEmpty(changelogRaw))
                    throw new Exception(@"No Changelog found...");

                var changelogFormatted = StripHtml(Markdown.ToHtml(changelogRaw))
                    .Replace("Full Changelog", string.Empty).Replace("Change Log", string.Empty);

                if (!string.IsNullOrEmpty(changelogFormatted))
                    return changelogFormatted;

                throw new Exception(@"No Changelog found...");
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        private static async Task DoDownloadAndInstallUpdate(IProgress<int> progress, CancellationToken ct)
        {
            CleanUpFiles(Directory.GetCurrentDirectory(), "*.old");

            var gitVersion = await GetGitHubVersion().ConfigureAwait(false);

            progress.Report(3);

            if (gitVersion <= Assembly.GetExecutingAssembly().GetName().Version)
                return;

            ct.ThrowIfCancellationRequested();

            var zipName = $"Celeste_Launcher_v{gitVersion}.zip";
            var downloadLink = $"{ReleaseZipUrl}{gitVersion}/{zipName}";

            //Download File
            progress.Report(5);

            var dowloadProgress = new Progress<DownloadFileProgress>();
            dowloadProgress.ProgressChanged += (o, ea) =>
            {
                progress.Report(5 + ea.ProgressPercentage * (65 - 5));
            };

            var tempFileName = Path.GetTempFileName();

            var downloadFileAsync = new DownloadFileUtils(new Uri(downloadLink), tempFileName, dowloadProgress);
            try
            {
                await downloadFileAsync.DoDownload(ct);
            }
            catch (AggregateException)
            {
                if (File.Exists(tempFileName))
                    File.Delete(tempFileName);

                throw;
            }

            //Extract File
            progress.Report(65);

            var extractProgress = new Progress<ZipFileProgress>();
            extractProgress.ProgressChanged += (o, ea) =>
            {
                progress.Report(65 + ea.ProgressPercentage * (90 - 65));
            };

            var tempDir = Path.Combine(Path.GetTempPath(), $"Celeste_Launcher_v{gitVersion}");

            if (Directory.Exists(tempDir))
                CleanUpFiles(tempDir, "*");

            try
            {
                await ZipUtils.DoExtractZipFile(tempFileName, tempDir, extractProgress, ct);
            }
            catch (AggregateException)
            {
                CleanUpFiles(tempDir, "*");
                throw;
            }
            finally
            {
                if (File.Exists(tempFileName))
                    File.Delete(tempFileName);
            }
            
            //Move File
            progress.Report(90);

            var destinationDir = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
            try
            {
                MoveFiles(tempDir, destinationDir);
            }
            finally
            {
                CleanUpFiles(tempDir, "*");
            }

            //Clean Old File
            progress.Report(97);
            CleanUpFiles(Directory.GetCurrentDirectory(), "*.old");

            //
            progress.Report(100);
        }

        public static void CleanUpFiles(string path, string pattern)
        {
            var files = new DirectoryInfo(path).GetFiles(pattern, SearchOption.AllDirectories);

            foreach (var file in files)
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception)
                {
                    //
                }
        }

        private static void MoveFiles(string originalPath, string destPath)
        {
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);

            foreach (var file in Directory.GetFiles(originalPath))
            {
                var name = Path.GetFileName(file);
                var dest = Path.Combine(destPath, name);

                if (File.Exists(dest))
                    File.Move(dest, dest + ".old");

                File.Copy(file, dest, true);
            }

            var folders = Directory.GetDirectories(originalPath);

            foreach (var folder in folders)
            {
                var name = Path.GetFileName(folder);
                if (name == null) continue;
                var dest = Path.Combine(destPath, name);
                MoveFiles(folder, dest);
            }
        }

        public void ProgressChanged(object sender, int e)
        {
            pB_Progress.Value = e;
        }

        private async void BtnSmall1_Click(object sender, EventArgs e)
        {
            try
            {
                btnSmall1.Enabled = false;
                pictureBoxButtonCustom1.Enabled = false;
                btnSmall1.BtnText = "...";

                _cts.Cancel();
                _cts = new CancellationTokenSource();

                var progress = new Progress<int>();
                progress.ProgressChanged += ProgressChanged;

                await DoDownloadAndInstallUpdate(progress, _cts.Token);

                MsgBox.ShowMessage(
                    @"""Celeste Launcher"" has been updated, it will now re-start.",
                    @"Project Celeste -- Updater",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(Assembly.GetEntryAssembly().Location);

                Environment.Exit(0);
            }
            catch (Exception exception)
            {
                MsgBox.ShowMessage(
                    $@"Error: {exception.Message}",
                    @"Project Celeste -- Updater",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Environment.Exit(0);
            }
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}