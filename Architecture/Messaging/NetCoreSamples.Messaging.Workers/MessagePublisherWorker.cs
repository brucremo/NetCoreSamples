using Microsoft.Extensions.Configuration;
using NetCoreSamples.Worker.Lib;
using Serilog;

namespace NetCoreSamples.Messaging.Workers
{
    public class MessagePublisherWorker : IWorker
    {
        public async Task Run(IConfiguration? configuration = null)
        {
            Log.Logger.Information("Publishing messages...");
            Thread.Sleep(1000);
        }
    }
}
