using Microsoft.Extensions.Configuration;

namespace NetCoreSamples.Worker.Lib.Tests.MockClasses
{
    internal class MockWorker : IWorker
    {
        public Task Run(IConfiguration? configuration = null)
        {
            return Task.CompletedTask;
        }
    }
}
