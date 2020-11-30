using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Northwind.Console.Constants;
using Northwind.Console.Interfaces.Services;
using Northwind.Console.Options;
using Northwind.Console.Services;
using Polly;
using Polly.Extensions.Http;

namespace Northwind.Web.Configuration
{
    public static class ConsoleServicesConfiguration
    {
        private const int RetryAttempts = 5;

        public static IServiceCollection AddConsoleServices(this IServiceCollection services)
        {
            return services
                .AddServices()
                .AddHttpClients();
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<INorthwindApiService, NorthwindApiService>();

            return services;
        }

        private static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            services
                .AddHttpClient(
                    HttpClientNames.NorthwindApiHttpClient,
                    (services, httpClient) =>
                    {
                        httpClient.BaseAddress = new Uri(services.GetService<IOptions<NorthwindApiOptions>>().Value.Path);
                    })
                .AddPolicyHandler(GetRetryPolicy());

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(RetryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
