using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreSamples.Broker.Lib.Options;
using NetCoreSamples.Broker.Lib.Services;

namespace NetCoreSamples.Broker.Lib.Extensions
{
    /// <summary>
    /// Broker extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void AddBrokerServices(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.Configure<BrokerServiceOptions>(configuration.GetSection("BrokerServiceOptions"));

            services.AddSingleton<IPubSubBrokerService, NatsBrokerService>();
            services.AddSingleton<IStreamBrokerService, NatsBrokerService>();
        }
    }
}
