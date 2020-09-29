using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using IoT.DotNetCore.BuildMonitor.Contracts;

namespace IoT.DotNetCore.BuildMonitor.Impl
{
    public class BuildClient : IBuildClient
    {
        public BuildClient(
            IMonitorConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IMonitorConfiguration Configuration { get; }

        public async Task<Runs> GetStatusAsync()
        {
            var uri = new Uri($"https://api.github.com/repos/{Configuration.Owner}/{Configuration.Repo}/actions/runs");
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", Configuration.Token);
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("BuildMonitor", "v0.1"));

            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));       

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Runs>(content);
        }
    }
}