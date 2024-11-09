using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.WorkerService
{
    public class ServiceWorker : BackgroundService
    {
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
