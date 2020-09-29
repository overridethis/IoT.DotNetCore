using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IoT.DotNetCore.BuildMonitor.Impl;
using IoT.DotNetCore.Hardware;
using IoT.DotNetCore.Hardware.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IoT.DotNetCore.BuildMonitor.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IBuildClient, BuildClient>();
            services.AddScoped<IMonitorConfiguration, MonitorConfiguration>();
            services.AddSingleton<IMonitorHardwareRunner>(svc =>
            {
                var cfg = svc.GetService<IConfiguration>();
                var pins = cfg.GetSection("Pins");
                var runner = new MonitorHardwareRunner(
                    new Button(pins.GetValue<int>("Button")),
                    new Buzzer(pins.GetValue<int>("Buzzer")),
                    new Lcd(),
                    new Led(pins.GetValue<int>("Led"))
                );
                return runner;
            });

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            var runner = serviceProvider.GetService<IMonitorHardwareRunner>();
            var service = serviceProvider.GetService<IBuildClient>();
            runner.Init(() =>
            {
                Task.Run(async () =>
                {
                    var response = await service.GetStatusAsync();
                    var run = response.WorkflowRuns.FirstOrDefault();
                    runner.Beep();
                    runner.Display(run);
                });
            });

        }
    }
}