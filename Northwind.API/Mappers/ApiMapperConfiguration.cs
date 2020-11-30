using AutoMapper;
using Northwind.Core.Web.Mappers;

namespace Northwind.Web.Mappers
{
    public static class ApiMapperConfiguration
    {
        public static IMapperConfigurationExpression ApplyApiMapperConfiguration(
            this IMapperConfigurationExpression configuration)
        {
            configuration
                .ApplyCoreWebMapperConfiguration();

            return configuration;
        }
    }
}
