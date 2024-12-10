using Microsoft.Extensions.Hosting;

namespace NetCoreSamples.Worker.Lib
{
    /// <summary>
    /// The Service Worker class
    /// </summary>
    public class ServiceWorker : BackgroundService
    {
        /// <summary>
        /// The Worker instance
        /// </summary>
        readonly IWorker Worker;

        public ServiceWorker(IWorker worker)
        {
            Worker = worker;
        }

        /// <summary>
        /// Executes the configured <see cref="IWorker"/> implementation as a background service
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Worker.Run(cancellationToken);
        }
    }
}
