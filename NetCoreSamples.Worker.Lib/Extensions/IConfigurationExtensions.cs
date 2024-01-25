using System.Reflection;

namespace NetCoreSamples.Worker.Lib.Extensions
{
    public static class IConfigurationExtensions
    {
        public static Type GetWorkerTypeByName(string workerName)
        {
            return Assembly.GetEntryAssembly()!
                .GetTypes()
                .FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IWorker)) && x.Name.Contains(workerName))!;
        }
    }
}
