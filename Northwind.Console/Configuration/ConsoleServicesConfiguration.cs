using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Northwind.Console.Interfaces.Services;
using Northwind.Console.Options;
using Northwind.Console.Services;

namespace Northwind.Web.Configuration
{
    public static class ConsoleServicesConfiguration
    {
        public static IServiceCollection AddConsoleServices(this IServiceCollection services)
        {
            return services
                .AddServices()
                .AddHttpClient();
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<INorthwindApiService, NorthwindApiService>();

            return services;
        }

        private static IServiceCollection AddHttpClient(this IServiceCollection services)
        {
            services.AddSingleton<HttpClient>(services => new HttpClient
            {
                BaseAddress = new Uri(services.GetService<IOptions<NorthwindApiOptions>>().Value.Path)
            });

            return services;
        }
    }
}
