namespace NetCoreSamples.Worker.Lib
{
    /// <summary>
    /// The basic interface for a Worker
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        /// Runs the Worker implementation tasks
        /// </summary>
        /// <returns></returns>
        Task Run();
    }
}
