using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Business.Configuration;
using Northwind.Web.Mappers;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Northwind.Web.Configuration
{
    public static class WebServicesConfiguration
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            return services
                .AddBusinessServices()
                .AddMapperConfiguration();
        }

        private static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
        {
            var mapperConfigurationExpression = new MapperConfigurationExpression()
                .ApplyWebMapperConfiguration() as MapperConfigurationExpression;

            services.AddSingleton<IConfigurationProvider>(_ => new MapperConfiguration(mapperConfigurationExpression));
            services.AddScoped<IMapper>(services => services.GetService<IConfigurationProvider>().CreateMapper());

            return services;
        }
    }
}
