namespace NetCoreSamples.Worker.Lib
{
    /// <summary>
    /// The basic interface for a Worker
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        /// Runs the Worker implementation
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Run(CancellationToken? cancellationToken = null);
    }
}
