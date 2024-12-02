using Microsoft.Extensions.Hosting;
using Serilog;

namespace NetCoreSamples.Worker.Lib
{
    /// <summary>
    /// The Service Worker class
    /// </summary>
    public class ServiceWorker : BackgroundService
    {
        /// <summary>
        /// The Worker instance
        /// </summary>
        private IWorker Worker;

        public ServiceWorker(IWorker worker)
        {
            this.Worker = worker;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Log.Logger.Information($"Starting @ UTC {DateTime.UtcNow}");

                    await this.Worker.Run();

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
}
