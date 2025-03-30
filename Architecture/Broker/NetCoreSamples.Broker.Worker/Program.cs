using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreSamples.Worker.Lib;
using System.Reflection;
using NetCoreSamples.Broker.Lib.Extensions;

namespace NetCoreSamples.Broker.Worker
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WorkerApplication.CreateServiceBuilder<WorkerOptions>(args)
                .WithCallingAssembly()
                .UseSerilog();

            ConfigureWorkers(builder);

            var app = builder.Build();
            await app.Run();
        }

        /// <summary>
        /// Method to configure all <see cref="IWorker"> implementations in the assembly as they use the same configuration pattern and services in this sample.
        /// </summary>
        /// <param name="builder">The <see cref="WorkerApplicationBuilder"/>.</param>
        static void ConfigureWorkers(WorkerApplicationBuilder builder)
        {
            Action<IServiceCollection, IConfiguration> ConfigureWorkersAction()
            {
                return (services, configuration) =>
                {
                    services.AddBrokerServices(configuration);
                };
            }

            var workerTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IWorker)
                .IsAssignableFrom(t))
                .Select(t => t.Name);

            foreach (var workerType in workerTypes)
            {
                builder.ConfigureWorker(workerType, ConfigureWorkersAction());
            }
        }
    }
}
