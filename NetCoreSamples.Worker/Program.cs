using Microsoft.Extensions.Configuration;
using NetCoreSamples.Logging.Lib;
using NetCoreSamples.Worker.Lib;
using Serilog;
using System.Reflection;
using NetCoreSamples.Worker.Lib.Extensions;

namespace NetCoreSamples.Worker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WorkerApplication.CreateBuilder(args);

            builder.WorkerAssembly = Assembly.Load("NetCoreSamples.Worker.Lib");

            builder.Configuration
                .AddJsonFile($"{AppDomain.CurrentDomain.BaseDirectory}appsettings.json");

            // Configure workers with extension on separate file
            builder.ConfigureWorkers();

            SerilogSetup.ConfigureSerilog(builder.Configuration);

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
