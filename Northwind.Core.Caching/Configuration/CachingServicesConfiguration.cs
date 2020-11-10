using Microsoft.Extensions.DependencyInjection;
using Northwind.Core.Caching.Interfaces;

namespace Northwind.Core.Caching.Configuration
{
    public static class CachingServicesConfiguration
    {
        public static IServiceCollection AddCachingServices(this IServiceCollection services)
        {
            return services.AddCaching();
        }

        private static IServiceCollection AddCaching(this IServiceCollection services)
        {
            services.AddTransient<IFilesCachingStorage, FileSystemFilesCachingStorage>();
            services.AddSingleton<IFilesCachingService, FilesCachingService>();

            return services;
        }
    }
}
