using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreSamples.Worker.Lib.SampleWorkers.One;
using NetCoreSamples.Worker.Lib.SampleWorkers.Two;
using System.Reflection;

namespace NetCoreSamples.Worker.Lib.Extensions
{
    public static class WorkerApplicationBuilderExtensions
    {
        // For the ad-hoc Worker project NetCoreSamples.Worker
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

        // For the WorkerService project NetCoreSamples.WorkerService
        public static void ConfigureWorkers(this HostApplicationBuilder builder)
        {
            List<Type> availableWorkers = Assembly.Load("NetCoreSamples.Worker.Lib")
                .GetTypes()
                .Where(a => a.GetInterfaces().Contains(typeof(IWorker)))
                .ToList();

            string workerName = builder.Configuration["Worker"];    

            Type requestedWorkerType = availableWorkers
                .FirstOrDefault(a => a.Name == workerName);

            if(requestedWorkerType == null)
            {
                throw new InvalidOperationException($"Worker {workerName} not found");
            }

            switch (workerName)
            {
                case "WorkerOne":
                    builder.Services.Configure<WorkerOneOptions>(builder.Configuration.GetSection(nameof(WorkerOneOptions)));
                    break;
                case "WorkerTwo":
                    builder.Services.Configure<WorkerTwoOptions>(builder.Configuration.GetSection(nameof(WorkerTwoOptions)));
                    break;
            }

            builder.Services.AddTransient(typeof(IWorker), requestedWorkerType);
        }
    }
}
