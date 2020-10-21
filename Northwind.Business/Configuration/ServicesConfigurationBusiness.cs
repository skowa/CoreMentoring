using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Northwind.Business.Interfaces.Providers;
using Northwind.Business.Interfaces.Services;
using Northwind.Business.Providers;
using Northwind.Business.Services;
using Northwind.Core.Options;
using Northwind.Data;
using Northwind.Data.EF;

namespace Northwind.Business.Configuration
{
    public static class ServicesConfigurationBusiness
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            return services
                .AddProviders()
                .AddServices()
                .AddUnitOfWork();
        }

        private static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<ICategoriesProvider, CategoriesProvider>();
            services.AddScoped<IProductsProvider, ProductsProvider>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IProductsService, ProductsService>();

            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWorkFactory<IUnitOfWork>>(p => new UnitOfWorkFactory(p.GetService<IOptions<ConnectionStringStore>>().Value.NorthwindDb));

            services.AddScoped<IUnitOfWork>(p => p.GetService<IUnitOfWorkFactory<IUnitOfWork>>().Create());

            return services;
        }
    }
}
