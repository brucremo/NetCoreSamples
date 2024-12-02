using Microsoft.Extensions.Configuration;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Messaging.Workers
{
    public class MessageProcessorWorker : IWorker
    {
        public async Task Run(IConfiguration? configuration = null)
        {
            Log.Logger.Information("Processing messages...");
            Thread.Sleep(5000);
        }
    }
}
