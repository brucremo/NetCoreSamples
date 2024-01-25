using Microsoft.Extensions.Options;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Worker.Workers.One
{
    public class WorkerOne : IWorker
    {
        private WorkerOneOptions Options { get; set; }

        public WorkerOne(IOptions<WorkerOneOptions> options)
        {
            this.Options = options.Value;
        }

        public async Task Run()
        {
            Log.Logger.Information($"WorkerOne says: {this.Options.TextToLog}");
        }
    }
}
