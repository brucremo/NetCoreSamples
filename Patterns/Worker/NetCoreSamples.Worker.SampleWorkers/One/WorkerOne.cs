using Microsoft.Extensions.Options;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Worker.SampleWorkers.One
{
    public class WorkerOne : IWorker
    {
        private WorkerOneOptions Options { get; set; }

        public WorkerOne(IOptions<WorkerOneOptions> options)
        {
            Options = options.Value;
        }

        public Task Run()
        {
            Log.Logger.Information($"WorkerOne says: {Options.TextToLog}");
            return Task.CompletedTask;
        }
    }
}
