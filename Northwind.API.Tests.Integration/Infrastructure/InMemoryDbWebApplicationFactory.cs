using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Northwind.API.Tests.Integration.Infrastructure.Data;
using Northwind.Data;

namespace Northwind.API.Tests.Integration.Infrastructure
{
    public class InMemoryDbWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IUnitOfWorkFactory<IUnitOfWork>));

                services.Remove(descriptor);

                services.AddScoped<IUnitOfWorkFactory<IUnitOfWork>>(_ => new TestUnitOfWorkFactory());
            });
        }
    }
}
