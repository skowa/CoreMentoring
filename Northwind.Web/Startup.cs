using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind.Core.Caching.Configuration;
using Northwind.Core.Caching.Options;
using Northwind.Core.Contants;
using Northwind.Core.Options;
using Northwind.Web.Configuration;
using Northwind.Web.Constants;
using Northwind.Web.Middlewares;
using Serilog;

namespace Northwind.Web
{
    public class Startup
    {
        private const string ErrorEndpoint = "/home/error";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStringStore>(Configuration.GetSection(ConfigurationProperties.ConnectionStringSection));
            services.Configure<ProductOptions>(Configuration.GetSection(ConfigurationProperties.ProductSection));
            services.Configure<AzureLogsAnalyticsWorkspace>(Configuration.GetSection(ConfigurationProperties.AzureLogsAnalyticsWorkspaceSection));
            services.Configure<CachingOptions>(Configuration.GetSection(ConfigurationProperties.CachingSection));

            services.AddWebServices();
            services.AddCachingServices();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger)
        {
            logger.Information(LogMessages.ApplicationStartedFrom, env.ContentRootPath);
            logger.Information(LogMessages.ConfigurationLoaded, Configuration.AsEnumerable());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(ErrorEndpoint);
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<ImageCachingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "image",
                    pattern: "images/{id}",
                    defaults: new { controller = "Categories", action = "Image" });
            });
        }
    }
}
