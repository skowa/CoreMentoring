using AutoMapper;
using Northwind.Core.Web.Mappers;

namespace Northwind.Web.Mappers
{
    public static class WebMapperConfiguration
    {
        public static IMapperConfigurationExpression ApplyWebMapperConfiguration(
            this IMapperConfigurationExpression configuration)
        {
            configuration
                .ApplyCoreWebMapperConfiguration();

            return configuration;
        }
    }
}
