using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreSamples.Worker.Lib;
using NetCoreSamples.Worker.Workers.One;
using NetCoreSamples.Worker.Workers.Two;
using Serilog;
using Serilog.Events;

namespace NetCoreSamples.Worker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WorkerApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile($"{AppDomain.CurrentDomain.BaseDirectory}\\appsettings.json");

            // Configure workers
            builder.ConfigureWorker("WorkerOne", (services, configuration) =>
            {
                services.Configure<WorkerOneOptions>(configuration.GetSection(nameof(WorkerOneOptions)));
            });

            builder.ConfigureWorker("WorkerTwo", (services, configuration) =>
            {
                services.Configure<WorkerTwoOptions>(configuration.GetSection(nameof(WorkerTwoOptions)));
            });

            // Configure logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.File(".\\Logs\\Worker-{Date}.log", LogEventLevel.Information)
                .CreateLogger();

            // Run the worker
            try
            {
                Log.Logger.Information($"Starting @ UTC {DateTime.UtcNow}");

                var app = builder.Build();

                await app.Run();

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
