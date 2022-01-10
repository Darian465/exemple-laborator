using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using Exemple.Events;
using Exemple.Events.ServiceBus;
using Exemple.Accomodation.EventProcessor;

namespace Example.Accomodation.EventProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddAzureClients(builder =>
                {
                    var connStr = Environment.GetEnvironmentVariable("ConnectionStrings:ServiceBus");
                    builder.AddServiceBusClient(connStr);
                });

                services.AddSingleton<IEventListener, ServiceBusTopicEventListener>();
                services.AddSingleton<IEventHandler, CosPublishedEventHandler>();

                services.AddHostedService<Worker>();
            });
    }
}