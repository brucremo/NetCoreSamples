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
        /// <summary>
        /// Adds broker services to the service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="configuration">The <see cref="IConfiguration"/></param>
        public static void AddBrokerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BrokerServiceOptions>(configuration.GetSection(nameof(BrokerServiceOptions)));

            services.AddSingleton<IPubSubBrokerService, NatsBrokerService>();
            services.AddSingleton<IStreamBrokerService, NatsBrokerService>();
        }
    }
}
