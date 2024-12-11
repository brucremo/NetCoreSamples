using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreSamples.Logging.Lib;
using System.Reflection;

namespace NetCoreSamples.Worker.Lib
{
    /// <summary>
    /// The <see cref="WorkerApplicationBuilder"/> is used to build a <see cref="WorkerApplication"/> for a Worker-based console application
    /// </summary>
    public class WorkerApplicationBuilder
    {
        /// <summary>
        /// The <see cref="IConfigurationManager"/> 
        /// </summary>
        public IConfigurationManager Configuration { get; set; } = new ConfigurationManager();

        /// <summary>
        /// The <see cref="IServiceCollection"/>
        /// </summary>
        public IServiceCollection Services
        {
            get
            {
                return HostApplicationBuilder?.Services ?? _services;
            }
            private set
            {
                _services = value;
            }
        }

        IServiceCollection _services = new ServiceCollection();

        /// <summary>
        /// Assembly where the Workers are located. Defaults to the EntryAssembly
        /// </summary>
        public Assembly? Assembly { get; private set; }

        /// <summary>
        /// The Worker type to be used
        /// </summary>
        Type? WorkerType { get; set; }

        /// <summary>
        /// A dictionary of Worker configurations that can be invoked by the --worker CLI parameter
        /// </summary>
        Dictionary<string, Action<IServiceCollection, IConfiguration>?> WorkerConfigurations { get; set; } = new();

        /// <summary>
        /// The <see cref="HostApplicationBuilder"/> to be used for a service-based worker application
        /// </summary>
        HostApplicationBuilder? HostApplicationBuilder { get; set; }

        internal WorkerApplicationBuilder()
        {
        }

        internal WorkerApplicationBuilder(HostApplicationBuilder hostApplicationBuilder)
        {
            HostApplicationBuilder = hostApplicationBuilder;
        }

        /// <summary>
        /// Adds a new preconfigured Worker and its DI setup to the <see cref="WorkerApplicationBuilder"/> that can be invoked
        /// </summary>
        /// <param name="workerName">The name of the Worker, matching the CLI parameter --worker / WorkerOptions:Name</param>
        /// <param name="action">The actual Worker setup with required services to be injected into the DI container </param>
        public void ConfigureWorker(string workerName, Action<IServiceCollection, IConfiguration>? action = null)
        {
            WorkerConfigurations.Add(workerName, action);
        }

        /// <summary>
        /// Builds the <see cref="WorkerApplication"/> for the preconfigured Worker setup requested by the --worker CLI parameter 
        /// </summary>
        /// <returns>A configured <see cref="WorkerApplication"/>.</returns>
        public WorkerApplication Build(string? workerNameConfiguration = null)
        {
            string workerName = Configuration[workerNameConfiguration ?? "WorkerOptions:Name"] ??
                throw new InvalidOperationException(
                    $"The WorkerOptions:Name cannot be null. Make sure the CLI parameter --worker is provided");

            if (WorkerType == null)
            {
                WorkerType = GetWorkerTypeByName(workerName) ?? throw new InvalidOperationException($"No Worker found with the name {workerName}");
            }

            Services.AddTransient((s) => (IWorker) ActivatorUtilities.CreateInstance(s, WorkerType));

            var workerSetupAction = WorkerConfigurations[workerName];

            if (workerSetupAction is not null)
            {
                workerSetupAction.Invoke(Services, Configuration);
            }

            Services.Configure<WorkerOptions>(Configuration.GetSection(nameof(WorkerOptions)));

            if (HostApplicationBuilder is not null)
            {
                return new WorkerApplication(Services.BuildServiceProvider(), HostApplicationBuilder.Build());
            }

            return new WorkerApplication(Services.BuildServiceProvider());
        }

        /// <summary>
        /// Sets the Worker assembly where the <see cref="IWorker"/> implementations exist to be used by the <see cref="WorkerApplicationBuilder"/>
        /// </summary>
        /// <param name="assembly">The assembly to be used</param>
        /// <returns>The builder instance</returns>
        public WorkerApplicationBuilder WithAssembly(Assembly assembly)
        {
            Assembly = assembly;
            return this;
        }

        /// <summary>
        /// Sets the Worker assembly where the <see cref="IWorker"/> implementations exist to be used by the <see cref="WorkerApplicationBuilder"/>
        /// </summary>
        /// <returns>The builder instance</returns>
        public WorkerApplicationBuilder WithCallingAssembly()
        {
            Assembly = Assembly.GetCallingAssembly();
            return this;
        }

        /// <summary>
        /// Sets the Worker type to be used by the <see cref="WorkerApplicationBuilder"/>
        /// </summary>
        /// <param name="typeName">The name of the Worker type</param>
        /// <returns>The builder instance</returns>
        public WorkerApplicationBuilder UseConfiguredWorkerType(string typeName)
        {
            WorkerType = GetWorkerTypeByName(typeName);
            return this;
        }

        /// <summary>
        /// Uses Serilog for logging
        /// </summary>
        /// <returns>The builder instance</returns>
        public WorkerApplicationBuilder UseSerilog()
        {
            SerilogSetup.ConfigureSerilog(Configuration);
            return this;
        }

        /// <summary>
        /// Returns the <see cref="Type"/> of the Worker requested by the --worker CLI parameter from the configured <see cref="Assembly"/>
        /// </summary>
        /// <param name="workerName">The Worker class name</param>
        /// <returns>The worker <see cref="Type"/></returns>
        Type? GetWorkerTypeByName(string workerName)
        {
            if (Assembly == null)
            {
                throw new InvalidOperationException("No Worker assembly configured. Use UseConfiguredAssembly to set the Worker assembly before calling UseConfiguredWorkerType");
            }

            return Assembly
                .GetTypes()
                .FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IWorker)) && x.Name.Contains(workerName))!;
        }
    }
}
