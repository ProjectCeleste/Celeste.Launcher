using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Celeste_Launcher_Gui.GitHub
{
    class GitHubClient
    {
        private const string LauncherRepoBaseUrl = "https://api.github.com/";

        private HttpClient _httpClient;

        public GitHubClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(LauncherRepoBaseUrl);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CelesteLauncher");
        }

        public async Task<ReleaseModel> GetLastReleaseDownloadLink(string repoOwner, string repoName)
        {
            var responseMessage = await _httpClient.GetAsync($"repos/{repoOwner}/{repoName}/releases/latest");

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ReleaseModel>(responseContent);
        }
    }
}
