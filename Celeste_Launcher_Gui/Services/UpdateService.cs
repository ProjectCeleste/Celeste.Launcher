using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.ViewModels;
using Celeste_Public_Api.Helpers;
using Markdig;
using System;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Celeste_Launcher_Gui.Services
{
    public class UpdateService
    {
        private const string AssemblyInfoUrl = "https://raw.githubusercontent.com/ProjectCeleste/Celeste_Launcher/master/Celeste_Launcher_Gui/Properties/AssemblyInfo.cs";

        private const string ChangelogUrl = "https://raw.githubusercontent.com/ProjectCeleste/Celeste_Launcher/master/CHANGELOG.md";

        private const string ReleaseZipUrl = "https://github.com/ProjectCeleste/Celeste_Launcher/releases/download/v";

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
                    throw new Exception(@"No Changelog found...");

                var changelogFormatted = StripHtml(Markdown.ToHtml(changelogRaw))
                    .Replace("Full Changelog", string.Empty).Replace("Change Log", string.Empty);

                if (!string.IsNullOrWhiteSpace(changelogFormatted))
                    return changelogFormatted;

                throw new Exception(@"No Changelog found...");
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public static async Task LoadUpdateInfo(LauncherVersionInfo launcherVersionInfo)
        {
            launcherVersionInfo.CurrentVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
            launcherVersionInfo.NewVersion = (await GetGitHubVersion()).ToString();
            launcherVersionInfo.ChangeLog = await GetChangeLog();
        }

        private static string StripHtml(string htmlText, bool decode = true)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(htmlText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }
    }
}
