using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Config
{
    public static class ServiceregistryAppExtension
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection service)
        {
            string consulAddress = "http://localhost:8500";
            service.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulconfig =>
            {
                consulconfig.Address = new Uri(consulAddress);
            }));

            return service;


        }
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtension");
            var lifeTime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            var registration = new AgentServiceRegistration()
            {
                ID = "Services",
                Name = "ServiceRegistryBarsha",
                Address = "localhost",
                Port = 24050
            };
            logger.LogInformation("Resistering with consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifeTime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Unregistering from Consul");

            });

            return app;
        }
    }
}
