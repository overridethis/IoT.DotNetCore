using System.Text.Json.Serialization;

namespace IoT.DotNetCore.BuildMonitor.Contracts
{
    public class RunWorkflow : Run
    {
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }
    }

}