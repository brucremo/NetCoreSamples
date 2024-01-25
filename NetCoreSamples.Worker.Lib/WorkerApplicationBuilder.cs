using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NetCoreSamples.Worker.Lib
{
    public class WorkerApplicationBuilder
    {
        public ConfigurationManager Configuration { get; set; } = new ConfigurationManager();
        public IServiceCollection Services { get; set; } = new ServiceCollection();
        private Dictionary<string, Action<IServiceCollection, IConfiguration>> WorkerConfigurations { get; set; } = new Dictionary<string, Action<IServiceCollection, IConfiguration>>();

        /// <summary>
        /// Adds a new Worker to the <see cref="WorkerApplicationBuilder"/> that can be invoked
        /// </summary>
        /// <param name="workerName">The name of the Worker, matching the CLI parameter --worker / WorkerBaseOptions:Worker</param>
        /// <param name="action">The actual Worker setup with required services to be injected into the DI container </param>
        public void ConfigureWorker(string workerName, Action<IServiceCollection, IConfiguration> action)
        {
            this.WorkerConfigurations.Add(workerName, action);
        }

        /// <summary>
        /// Builds the <see cref="WorkerApplication"/> for the Worker requested by the --worker CLI parameter 
        /// </summary>
        /// <returns>A configured <see cref="WorkerApplication"/>.</returns>
        public WorkerApplication Build()
        {
            string workerName = this.Configuration["WorkerBaseOptions:Worker"]!;

            if(workerName == null)
            {
                throw new InvalidOperationException(
                    $"The WorkerBaseOptions:Worker cannot be null. Make sure the CLI parameter --worker is provided");
            }

            Type workerType = this.GetWorkerTypeByName(workerName);

            if (workerType == null)
            {
                throw new InvalidOperationException(
                    $"No Worker found with the name {workerName}");
            }

            this.Services.AddTransient((s) => (IWorker) ActivatorUtilities.CreateInstance(s, workerType));

            this.WorkerConfigurations[workerName]
                .Invoke(Services, this.Configuration);

            return new WorkerApplication(
                this.Services.BuildServiceProvider());
        }

        private Type GetWorkerTypeByName(string workerName)
        {
            return Assembly.GetEntryAssembly()!
                .GetTypes()
                .FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IWorker)) && x.Name.Contains(workerName))!;
        }
    }
}
