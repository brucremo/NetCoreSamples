using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreSamples.Logging.Lib;
using NetCoreSamples.Worker.Lib;
using Serilog;
using System.Reflection;

namespace NetCoreSamples.GenericWorker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            switch (Environment.GetEnvironmentVariable("WORKER_MODE")?.ToLower() ?? throw new InvalidOperationException("No value found for WORKER_MODE"))
            {
                case "worker":
                    await StartAdHocWorker(args);
                    break;
                case "service":
                    StartServiceWorker(args);
                    break;
                default:
                    throw new InvalidOperationException("Invalid argument. Set 'worker' or 'service' as WORKER_MODE");
            }
        }

        /// <summary>
        /// Starts a service worker
        /// </summary>
        /// <param name="args">The CLI args</param>
        /// <exception cref="InvalidOperationException"></exception>
        static void StartServiceWorker(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Configuration
                .AddCommandLine(args, WorkerApplication.BuildSwitchMap<WorkerOptions>())
                .AddEnvironmentVariables();

            SerilogSetup.ConfigureSerilog(builder.Configuration);

            string assemblyName = builder.Configuration["WorkerOptions:Assembly"] ?? throw new InvalidOperationException("No Worker Assembly provided!");
            string workerName = builder.Configuration["WorkerOptions:Name"] ?? throw new InvalidOperationException("No Worker name provided!");

            List<Type> availableWorkers = Assembly.LoadFrom(assemblyName)
                .GetTypes()
                .Where(a => a.GetInterfaces().Contains(typeof(IWorker)))
                .ToList();

            Type requestedWorkerType = availableWorkers
                .FirstOrDefault(a => a.Name == workerName) ?? throw new InvalidOperationException($"No Worker found with name {workerName}");

            builder.Services.AddSingleton(typeof(IWorker), requestedWorkerType);

            builder.Services.AddHostedService<ServiceWorker>();

            var host = builder.Build();

            try
            {
                Log.Logger.Information($"Starting @ UTC {DateTime.UtcNow}");

                host.Run();

                Log.Logger.Information($"Finished @ UTC {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error: {ex.Message}");
                Log.Logger.Error($"Trace: {ex.StackTrace}");
                Log.Logger.Information($"Finished @ UTC {DateTime.UtcNow}");

                throw;
            }
        }

        /// <summary>
        /// Start an ad-hoc worker
        /// </summary>
        /// <param name="args">The CLI args</param>
        /// <returns></returns>
        static async Task StartAdHocWorker(string[] args)
        {
            var builder = WorkerApplication
                .CreateBuilder(args)
                .WithConfiguredAssembly("WorkerOptions:Assembly");

            SerilogSetup.ConfigureSerilog(builder.Configuration);

            try
            {
                Log.Logger.Information($"Starting @ UTC {DateTime.UtcNow}");

                await builder.BuildGeneric().Run(builder.Configuration);

                Log.Logger.Information($"Finished @ UTC {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error: {ex.Message}");
                Log.Logger.Error($"Trace: {ex.StackTrace}");
                Log.Logger.Information($"Finished @ UTC {DateTime.UtcNow}");

                throw;
            }
        }
    }
}
