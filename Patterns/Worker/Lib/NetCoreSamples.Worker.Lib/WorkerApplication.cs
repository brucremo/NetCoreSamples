using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

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
        IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// The Host instance used for service-based worker applications
        /// </summary>
        IHost? HostApp { get; set; }

        internal WorkerApplication(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        internal WorkerApplication(IServiceProvider serviceProvider, IHost host)
        {
            ServiceProvider = serviceProvider;
            HostApp = host;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerApplicationBuilder"/> class with preconfigured defaults.
        /// </summary>
        /// <returns>The <see cref="WorkerApplicationBuilder"/>.</returns>
        public static WorkerApplicationBuilder CreateBuilder()
        {
            var builder = new WorkerApplicationBuilder();

            builder.Configuration
                .AddEnvironmentVariables();

            return builder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerApplicationBuilder"/> class with preconfigured defaults.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The <see cref="WorkerApplicationBuilder"/>.</returns>
        public static WorkerApplicationBuilder CreateBuilder<T>(string[] args)
        {
            var builder = new WorkerApplicationBuilder();

            builder.Configuration
                .AddCommandLine(args, BuildSwitchMap<T>())
                .AddEnvironmentVariables();

            return builder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerApplicationBuilder"/> class with preconfigured defaults for a service-based worker application
        /// </summary>
        /// <typeparam name="T">The worker options type for BuildSwitchMap</typeparam>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The <see cref="WorkerApplicationBuilder"/>.</returns>
        public static WorkerApplicationBuilder CreateServiceBuilder<T>(string[] args)
        {
            var builder = new WorkerApplicationBuilder(Host.CreateApplicationBuilder(args));

            builder.Configuration
                .AddCommandLine(args, BuildSwitchMap<T>())
                .AddEnvironmentVariables();

            builder.Services.AddHostedService<ServiceWorker>();

            return builder;
        }

        /// <summary>
        /// Runs the Worker Application
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            try
            {
                Log.Logger.Information($"Starting @ UTC {DateTime.UtcNow}");

                if (HostApp is null)
                {
                    await ServiceProvider.GetRequiredService<IWorker>().Run();
                }
                else
                {
                    HostApp.Run();
                }

                Log.Logger.Information($"Finished @ UTC {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error: {ex.Message}");
                Log.Logger.Error($"Trace: {ex.StackTrace}");
                Log.Logger.Information($"Finished @ UTC {DateTime.UtcNow}");

                throw;
            }
        }

        /// <summary>
        /// Builds SwitchMap support for CLI parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static Dictionary<string, string> BuildSwitchMap<T>()
        {
            return typeof(T)
                .GetProperties()
                .ToDictionary(a => $"--{a.Name}", b => $"{typeof(T).Name}:{b.Name}");
        }
    }
}
