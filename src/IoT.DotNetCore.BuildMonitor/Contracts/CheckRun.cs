using System;
using System.Text.Json.Serialization;

namespace IoT.DotNetCore.BuildMonitor.Contracts
{
    public class CheckRun : Run
    {
        [JsonPropertyName("completed_at")]
        public DateTime? CompletedAt { get; set; }

        [JsonPropertyName("started_at")]
        public DateTime? StartedAt { get; set; }

    }
}

