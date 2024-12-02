using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        /// The DI container
        /// </summary>
        public IServiceCollection Services { get; set; } = new ServiceCollection();

        /// <summary>
        /// Assembly where the Workers are located. Defaults to the EntryAssembly
        /// </summary>
        public Assembly? Assembly { get; set; }

        /// <summary>
        /// The Worker type to be used
        /// </summary>
        private Type? WorkerType { get; set; }

        /// <summary>
        /// A dictionary of Worker configurations that can be invoked by the --worker CLI parameter
        /// </summary>
        private Dictionary<string, Action<IServiceCollection, IConfiguration>?> WorkerConfigurations { get; set; } = new();

        /// <summary>
        /// Adds a new preconfigured Worker and its DI setup to the <see cref="WorkerApplicationBuilder"/> that can be invoked
        /// </summary>
        /// <param name="workerName">The name of the Worker, matching the CLI parameter --worker / WorkerOptions:Name</param>
        /// <param name="action">The actual Worker setup with required services to be injected into the DI container </param>
        public void ConfigureWorker(string workerName, Action<IServiceCollection, IConfiguration>? action = null)
        {
            this.WorkerConfigurations.Add(workerName, action);
        }

        /// <summary>
        /// Builds the <see cref="WorkerApplication"/> for a generic Worker configured at runtime
        /// </summary>
        /// <returns>A configured <see cref="WorkerApplication"/>.</returns>
        public WorkerApplication BuildGeneric()
        {
            this.Services.AddTransient((s) => (IWorker)ActivatorUtilities
                .CreateInstance(s, WorkerType ?? throw new InvalidOperationException($"No Worker type setup. Please call UseConfiguredWorkerType")));

            return new WorkerApplication(
                this.Services.BuildServiceProvider());
        }

        /// <summary>
        /// Builds the <see cref="WorkerApplication"/> for the preconfigured Worker setup requested by the --worker CLI parameter 
        /// </summary>
        /// <returns>A configured <see cref="WorkerApplication"/>.</returns>
        public WorkerApplication BuildWithNamedWorkers()
        {
            string workerName = this.Configuration["WorkerOptions:Name"] ??
                throw new InvalidOperationException(
                    $"The WorkerOptions:Name cannot be null. Make sure the CLI parameter --worker is provided");

            if (WorkerType == null)
            {
                WorkerType = GetWorkerTypeByName(workerName) ?? throw new InvalidOperationException($"No Worker found with the name {workerName}");
            }

            this.Services.AddTransient((s) => (IWorker) ActivatorUtilities.CreateInstance(s, WorkerType));

            this.WorkerConfigurations[workerName]!
                .Invoke(Services, this.Configuration);

            return new WorkerApplication(
                this.Services.BuildServiceProvider());
        }

        /// <summary>
        /// Sets the Worker assembly to be used by the <see cref="WorkerApplicationBuilder"/>
        /// </summary>
        /// <param name="configurationKeyValue">The configuration name with the assembly to be used</param>
        /// <returns>The builder instance</returns>
        public WorkerApplicationBuilder WithConfiguredAssembly(string configurationKeyValue)
        {
            Assembly = Assembly.Load(
                Configuration.GetValue<string>(configurationKeyValue) ?? throw new InvalidOperationException($"There's no value configured for {configurationKeyValue}"));
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
        /// Returns the <see cref="Type"/> of the Worker requested by the --worker CLI parameter from the configured <see cref="Assembly"/>
        /// </summary>
        /// <param name="workerName">The Worker class name</param>
        /// <returns>The worker <see cref="Type"/></returns>
        private Type? GetWorkerTypeByName(string workerName)
        {
            if (this.Assembly == null)
            {
                throw new InvalidOperationException("No Worker assembly configured. Use UseConfiguredAssembly to set the Worker assembly before calling UseConfiguredWorkerType");
            }

            return this.Assembly
                .GetTypes()
                .FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IWorker)) && x.Name.Contains(workerName))!;
        }
    }
}
