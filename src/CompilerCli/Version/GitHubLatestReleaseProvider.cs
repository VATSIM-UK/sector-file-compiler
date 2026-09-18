using System;
using System.Net.Http;
using System.Text.Json;

namespace CompilerCli.Version
{
    public class GitHubLatestReleaseProvider : ILatestReleaseProvider
    {
        private const string ReleasesUrl = "https://api.github.com/repos/VATSIM-UK/sector-file-compiler/releases/latest";

        public string GetLatestReleaseTag()
        {
            try
            {
                using var client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(3)
                };
                client.DefaultRequestHeaders.UserAgent.ParseAdd("sector-file-compiler-cli");

                using HttpResponseMessage response = client.GetAsync(ReleasesUrl).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                string body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                using JsonDocument document = JsonDocument.Parse(body);
                if (!document.RootElement.TryGetProperty("tag_name", out JsonElement tagNameElement))
                {
                    return null;
                }

                return tagNameElement.GetString();
            }
            catch
            {
                return null;
            }
        }
    }
}
