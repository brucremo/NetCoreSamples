using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using NetCoreSamples.Broker.Lib.Options;
using NetCoreSamples.Broker.Lib.Services.Nats;
using NetCoreSamples.DependencyInjection.Lib.Extensions;

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
            services.ConfigureOptionsType<BrokerServiceOptions>(configuration);
            services.AddSingleton<IHostEnvironment, HostingEnvironment>();

            services.AddInterfaceHostedService<INatsBrokerService, NatsBrokerService>();

            services.AddSingleton<IPubSubBrokerService, NatsBrokerService>();
            services.AddSingleton<IQueueBrokerService, NatsBrokerService>();
        }
    }
}
