using Microsoft.Extensions.DependencyInjection;
using NetCoreSamples.Worker.Lib;
using NetCoreSamples.Worker.Workers.One;
using NetCoreSamples.Worker.Workers.Two;

namespace NetCoreSamples.Worker.Extensions
{
    public static class WorkerApplicationBuilderExtensions
    {
        public static void ConfigureWorkers(this WorkerApplicationBuilder builder)
        {
            builder.ConfigureWorker("WorkerOne", (services, configuration) =>
            {
                services.Configure<WorkerOneOptions>(configuration.GetSection(nameof(WorkerOneOptions)));
            });

            builder.ConfigureWorker("WorkerTwo", (services, configuration) =>
            {
                services.Configure<WorkerTwoOptions>(configuration.GetSection(nameof(WorkerTwoOptions)));
            });
        }
    }
}
