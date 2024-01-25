using Microsoft.Extensions.Options;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Worker.Workers.Two
{
    public class WorkerTwo : IWorker
    {
        private WorkerTwoOptions Options { get; set; }

        public WorkerTwo(IOptions<WorkerTwoOptions> options)
        {
            this.Options = options.Value;
        }

        public async Task Run()
        {
            Log.Logger.Information($"WorkerTwo says: {this.Options.TextToLog}");
        }
    }
}
