using Microsoft.Extensions.Configuration;
using NetCoreSamples.Worker.Extensions;
using NetCoreSamples.Worker.Lib;
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

            // Configure workers with extension on separate file
            builder.ConfigureWorkers();

            // Configure logging
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
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
