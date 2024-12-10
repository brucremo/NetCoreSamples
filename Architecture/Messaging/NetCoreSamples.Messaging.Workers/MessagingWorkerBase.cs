using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using NetCoreSamples.Messaging.Lib.Services;
using NetCoreSamples.Worker.Lib;

namespace NetCoreSamples.Messaging.Workers
{
    public class MessagingWorkerBase : IWorker
    {
        private WorkerApplicationBuilder Builder { get; set; }

        protected IConfiguration Configuration => Builder.Configuration;
        protected IServiceCollection Services => Builder.Services;

        public MessagingWorkerBase()
        {
            Builder = WorkerApplication.CreateBuilder()
                .WithCallingAssembly();

            Services.Configure<MessageHubClientOptions>(Configuration.GetSection("MessageHubClientOptions"));

            Builder.ConfigureWorker("MessagePublisherWorker", (services, configuration) =>
            {
                services.Configure<MessageHubClientOptions>(configuration.GetSection(nameof(MessageHubClientOptions)));
                services.AddSingleton<IHostEnvironment, HostingEnvironment>(p => new HostingEnvironment());
                services.AddSingleton<IMessageHubClientService, MessageHubClientService>();
                services.AddHostedService(p => p.GetRequiredService<IMessageHubClientService>());
            });
        }

        public async Task Run()
        {
            await Builder.Build("MessagingWorkerOptions:Name").Run();
        }
    }
}
