using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NetCoreSamples.DependencyInjection.Lib.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a hosted service implementation to the service collection.
        /// </summary>
        /// <typeparam name="TService">The service interface.</typeparam>
        /// <typeparam name="TImplementation">The service implementation.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        public static void AddInterfaceHostedService<TService, TImplementation>(this IServiceCollection services)
            where TService : class, IHostedService
            where TImplementation : class, TService, IHostedService
        {
            services.AddSingleton<TService, TImplementation>();
            services.AddHostedService(p => p.GetRequiredService<TService>());
        }
    }
}
