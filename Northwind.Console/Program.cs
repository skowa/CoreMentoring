using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Console.Constants;
using Northwind.Console.Interfaces.Services;
using Northwind.Console.Options;
using Northwind.Web.Configuration;

namespace Northwind.Console
{
    public class Program
    {
        private const string AppSettingsFileName = "appsettings.json";

        public static async Task Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(AppSettingsFileName)
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddConsoleServices();

            serviceCollection.AddOptions();
            serviceCollection.Configure<NorthwindApiOptions>(configuration.GetSection(ConfigSections.NorthwindApi));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            await NorthwindApiConsolePresenter.RunAsync(serviceProvider.GetService<INorthwindApiService>());
        }
    }
}
