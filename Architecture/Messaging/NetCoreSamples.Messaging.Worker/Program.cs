using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Hosting;
using NetCoreSamples.Messaging.Lib.Services;
using NetCoreSamples.Worker.Lib;
using NetCoreSamples.DependencyInjection.Lib.Extensions;

namespace NetCoreSamples.Messaging.Worker
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WorkerApplication.CreateServiceBuilder<WorkerOptions>(args)
                .WithCallingAssembly()
                .UseSerilog();

            builder.ConfigureWorker("MessageProcessor", BaseConfigureWorkersAction());
            builder.ConfigureWorker("MessagePublisher", BaseConfigureWorkersAction());

            var app = builder.Build();
            await app.Run();
        }

        static Action<IServiceCollection, IConfiguration> BaseConfigureWorkersAction()
        {
            return (services, configuration) =>
            {
                services.Configure<MessageHubClientOptions>(configuration.GetSection(nameof(MessageHubClientOptions)));
                services.AddSingleton<IHostEnvironment, HostingEnvironment>();
                services.AddInterfaceHostedService<IMessageHubClientService, MessageHubClientService>();
            };
        }
    }
}
