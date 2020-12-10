using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind.Core.Caching.Configuration;
using Northwind.Core.Caching.Options;
using Northwind.Core.Constants;
using Northwind.Core.Options;
using Northwind.Data.EF;
using Northwind.Web.Configuration;
using Northwind.Web.Constants;
using Northwind.Web.Filters;
using Northwind.Web.Middlewares;
using Serilog;

namespace Northwind.Web
{
    public class Startup
    {
        private const string ErrorEndpoint = "/home/error";
        private const string AccessDeniedEndpoint = "/Account/AccessDenied";
        private const string GitHubProvider = "GitHub";
        private const string GitHubClientId = "ClientId";
        private const string GitHubClientSecret = "ClientSecret";
        private const string UserEmailScope = "user:email";

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
            services.Configure<LoggingOptions>(Configuration.GetSection(ConfigurationProperties.LoggingSection));
            services.Configure<SmtpSettings>(Configuration.GetSection(ConfigurationProperties.SmtpSettingsSection));

            services.AddWebServices();
            services.AddCachingServices();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserStoreContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = AccessDeniedEndpoint;
            });

            services.AddAuthentication()
                .AddGitHub(GitHubProvider, options =>
                {
                    options.ClientId = Configuration[$"{ConfigurationProperties.GitHubSection}:{GitHubClientId}"];
                    options.ClientSecret = Configuration[$"{ConfigurationProperties.GitHubSection}:{GitHubClientSecret}"];
                    options.Scope.Add(UserEmailScope);
                })
                // Our scenario doesn't work with the new library that was referenced in module.
                // There is a bug, that is opened in guthub https://github.com/AzureAD/microsoft-identity-web/issues/133#issuecomment-737204100
                // So, the old library is needed to be used.
                .AddAzureAD(options => Configuration.Bind(ConfigurationProperties.AzureAdSection, options));

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<LoggingActionFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ILogger logger)
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ImageCachingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "image",
                    pattern: string.Format(Routes.ImagesRoute, "{id}"),
                    defaults: new { controller = "Categories", action = "Image" });
            });

            if (env.IsDevelopment())
            {
                UserStoreConfiguration.SeedDataAsync(serviceProvider).GetAwaiter().GetResult();
            }
        }
    }
}
