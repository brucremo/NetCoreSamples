using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Hosting;
using NetCoreSamples.Worker.Lib;
using NetCoreSamples.DependencyInjection.Lib.Extensions;
using NetCoreSamples.Broker.Lib.Options;
using NetCoreSamples.Broker.Lib;
using NetCoreSamples.Broker.Lib.Services;
using NetCoreSamples.Broker.Lib.Services.Nats;

namespace NetCoreSamples.Broker.Worker
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WorkerApplication.CreateServiceBuilder<WorkerOptions>(args)
                .WithCallingAssembly()
                .UseSerilog();

            builder.ConfigureWorker("Processor", BaseConfigureWorkersAction());
            builder.ConfigureWorker("Publisher", BaseConfigureWorkersAction());

            var app = builder.Build();
            await app.Run();
        }

        static Action<IServiceCollection, IConfiguration> BaseConfigureWorkersAction()
        {
            return (services, configuration) =>
            {
                services.ConfigureOptionsType<BrokerServiceOptions>(configuration);
                services.AddSingleton<IHostEnvironment, HostingEnvironment>();

                services.AddSingleton<IPubSubBrokerService, NatsBrokerService>();
                services.AddSingleton<IStreamBrokerService, NatsBrokerService>();
                services.AddInterfaceHostedService<INatsBrokerService, NatsBrokerService>();
            };
        }
    }
}
