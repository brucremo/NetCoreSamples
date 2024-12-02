using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;

namespace NetCoreSamples.Worker.Lib.SampleWorkers.Two
{
    public class WorkerTwo : IWorker, IHostedService
    {
        private WorkerTwoOptions Options { get; set; }

        public WorkerTwo(IOptions<WorkerTwoOptions> options)
        {
            this.Options = options.Value;
        }

        public Task Run(IConfiguration? configuration = null)
        {
            Log.Logger.Information($"WorkerTwo says: {this.Options.TextToLog}");
            Thread.Sleep(this.Options.DelayMiliseconds);
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Logger.Information($"WorkerTwo says: {this.Options.TextToLog}");
            Thread.Sleep(this.Options.DelayMiliseconds);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
