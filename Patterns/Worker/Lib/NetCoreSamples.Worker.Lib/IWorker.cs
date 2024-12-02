using Microsoft.Extensions.Configuration;

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
        /// <param name="configuration">The configuration</param>
        /// <returns></returns>
        Task Run(IConfiguration? configuration = null);
    }
}
