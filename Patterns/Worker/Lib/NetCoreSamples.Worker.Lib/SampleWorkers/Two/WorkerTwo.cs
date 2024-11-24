using Microsoft.Extensions.Options;
using Serilog;

namespace NetCoreSamples.Worker.Lib.SampleWorkers.Two
{
    public class WorkerTwo : IWorker
    {
        private WorkerTwoOptions Options { get; set; }

        public WorkerTwo(IOptions<WorkerTwoOptions> options)
        {
            this.Options = options.Value;
        }

        public Task Run()
        {
            Log.Logger.Information($"WorkerTwo says: {this.Options.TextToLog}");
            Thread.Sleep(this.Options.DelayMiliseconds);
            return Task.CompletedTask;
        }
    }
}
