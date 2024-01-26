using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreSamples.Worker.Lib
{
    /// <summary>
    /// A Worker Application containing multiple injected Workers implementing <see cref="IWorker"/>
    /// </summary>
    public class WorkerApplication
    {
        /// <summary>
        /// The Service Provider
        /// </summary>
        private IServiceProvider ServiceProvider { get; set; }

        public WorkerApplication(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerApplicationBuilder"/> class with preconfigured defaults.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The <see cref="WorkerApplicationBuilder"/>.</returns>
        public static WorkerApplicationBuilder CreateBuilder(string[] args)
        {
            var builder = new WorkerApplicationBuilder();

            builder.Configuration
                .AddCommandLine(args, BuildSwitchMap<WorkerBaseOptions>());

            return builder;
        }

        /// <summary>
        /// Builds SwitchMap support for CLI parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static Dictionary<string, string> BuildSwitchMap<T>()
        {
            return typeof(T)
                .GetProperties()
                .ToDictionary(a => $"--{a.Name}", b => $"{typeof(T).Name}:{b.Name}");
        }

        /// <summary>
        /// Runs the Worker Application
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            await this.ServiceProvider.GetService<IWorker>()!.Run();
        }
    }
}
