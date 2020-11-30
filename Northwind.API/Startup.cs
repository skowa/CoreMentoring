using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Northwind.API.Configuration;
using Northwind.Core.Constants;
using Northwind.Core.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Northwind.API
{
    public class Startup
    {
        private const string SwaggerEndpoint = "/swagger/v1/swagger.json";
        private const string ApiName = "Northwind API";
        private const string FileType = "file";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStringStore>(Configuration.GetSection(ConfigurationProperties.ConnectionStringSection));

            services.AddApiServices();

            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.MapType<FileContentResult>(() => new OpenApiSchema { Type = FileType });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(SwaggerEndpoint, ApiName);
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
