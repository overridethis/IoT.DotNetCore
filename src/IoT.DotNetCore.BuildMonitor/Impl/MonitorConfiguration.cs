using System;
using Microsoft.Extensions.Configuration;

namespace IoT.DotNetCore.BuildMonitor.Impl
{
    public class MonitorConfiguration : IMonitorConfiguration
    {
        private IConfiguration Configuration { get; }

        public MonitorConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string Owner => Configuration["GitHub:Owner"];
        public string Repo => Configuration["GitHub:Repo"];
        public string Token => Configuration["GitHub:Token"];
    }
}
