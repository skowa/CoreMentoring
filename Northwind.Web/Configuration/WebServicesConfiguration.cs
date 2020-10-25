using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Northwind.Business.Configuration;
using Northwind.Core.Options;
using Northwind.Web.Enrichers;
using Northwind.Web.Mappers;
using Serilog;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Northwind.Web.Configuration
{
    public static class WebServicesConfiguration
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            return services
                .AddBusinessServices()
                .AddMapperConfiguration()
                .AddLogger();
        }

        private static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
        {
            var mapperConfigurationExpression = new MapperConfigurationExpression()
                .ApplyWebMapperConfiguration() as MapperConfigurationExpression;

            services.AddSingleton<IConfigurationProvider>(_ => new MapperConfiguration(mapperConfigurationExpression));
            services.AddScoped<IMapper>(services => services.GetService<IConfigurationProvider>().CreateMapper());

            return services;
        }

        private static IServiceCollection AddLogger(this IServiceCollection services)
        {
            services.AddSingleton(services =>
                {
                    var azureAnalytics = services.GetService<IOptions<AzureLogsAnalyticsWorkspace>>().Value;

                    return new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .Enrich.With<LogIdEnricher>()
                        .WriteTo.AzureAnalytics(azureAnalytics.WorkspaceId, azureAnalytics.PrimaryKey);
                });

            services.AddSingleton<ILogger>(services => services.GetService<LoggerConfiguration>().CreateLogger());

            return services;
        }
    }
}
