﻿namespace NetCoreSamples.Worker.Lib.Tests.MockClasses
{
    internal class MockWorker : IWorker
    {
        public Task Run(CancellationToken? cancellationToken = null)
        {
            return Task.CompletedTask;
        }
    }
}
