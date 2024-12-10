using Microsoft.Extensions.Options;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Worker.SampleWorkers.Two
{
    public class WorkerTwo : IWorker
    {
        private WorkerTwoOptions Options { get; set; }

        public WorkerTwo(IOptions<WorkerTwoOptions> options)
        {
            Options = options.Value;
        }

        public Task Run()
        {
            Log.Logger.Information($"WorkerTwo says: {Options.TextToLog}");
            return Task.CompletedTask;
        }
    }
}
