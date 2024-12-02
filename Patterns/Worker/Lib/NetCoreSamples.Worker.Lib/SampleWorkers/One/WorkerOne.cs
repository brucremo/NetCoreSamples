using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;

namespace NetCoreSamples.Worker.Lib.SampleWorkers.One
{
    public class WorkerOne : IWorker
    {
        private WorkerOneOptions Options { get; set; }

        public WorkerOne(IOptions<WorkerOneOptions> options)
        {
            this.Options = options.Value;
        }

        public Task Run(IConfiguration? configuration = null)
        {
            Log.Logger.Information($"WorkerOne says: {this.Options.TextToLog}");
            return Task.CompletedTask;
        }
    }
}
