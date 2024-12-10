namespace NetCoreSamples.Worker.Lib.Tests.MockClasses
{
    internal class MockWorker : IWorker
    {
        public Task Run()
        {
            return Task.CompletedTask;
        }
    }
}
