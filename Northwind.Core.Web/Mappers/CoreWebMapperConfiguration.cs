using AutoMapper;
using Northwind.Business.Mappers;

namespace Northwind.Web.Mappers
{
    public static class CoreWebMapperConfiguration
    {
        public static IMapperConfigurationExpression ApplyCoreWebMapperConfiguration(
            this IMapperConfigurationExpression configuration)
        {
            configuration
                .ApplyBusinessMapperConfiguration()
                .ApplyCategoryModelMapping()
                .ApplyProductModelMapping();

            return configuration;
        }
    }
}
