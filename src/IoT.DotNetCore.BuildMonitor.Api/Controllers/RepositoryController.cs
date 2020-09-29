using System.Text.Json;
using System.Threading.Tasks;
using IoT.DotNetCore.BuildMonitor.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IoT.DotNetCore.BuildMonitor.Api.Controllers
{
    [Route("repo")]
    public class RepositoryController : Controller
    {
        private readonly IBuildClient _buildClient;
        private readonly ILogger<RepositoryController> _logger;
        private readonly IMonitorHardwareRunner _runner;

        public RepositoryController(
            IBuildClient buildClient,
            ILogger<RepositoryController> logger,
            IMonitorHardwareRunner runner)
        {
            _buildClient = buildClient;
            _logger = logger;
            _runner = runner;
        }

        [HttpPost]
        [Route("hooks")]
        public IActionResult Post([FromBody] RunHook hookModel)
        {
            _logger.Log(LogLevel.Information, JsonSerializer.Serialize(hookModel));
            _runner.Display(hookModel.CheckRun);
            return Ok(hookModel);
        }

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> GetAsync() => Ok(await _buildClient.GetStatusAsync());
            
    }
}

