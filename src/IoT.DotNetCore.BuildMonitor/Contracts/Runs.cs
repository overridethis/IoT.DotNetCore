using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IoT.DotNetCore.BuildMonitor.Contracts
{
    public class Runs
    {
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("workflow_runs")]
        public IEnumerable<RunWorkflow> WorkflowRuns { get; set; }
    }    
}