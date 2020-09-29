using System.Text.Json.Serialization;

namespace IoT.DotNetCore.BuildMonitor.Contracts
{
    public class RunHook
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("check_run")]
        public CheckRun CheckRun { get; set; }
    }
}

