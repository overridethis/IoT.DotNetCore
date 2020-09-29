using System.Text.Json.Serialization;

namespace IoT.DotNetCore.BuildMonitor.Contracts
{
    public class Run
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("conclusion")]
        public string Conclusion { get; set; }

        public bool IsFailure() => Status == "failure";
        
        public bool IsCompleted() => Status == "completed";

        public bool IsQueued() => Status == "queued";
    }
}