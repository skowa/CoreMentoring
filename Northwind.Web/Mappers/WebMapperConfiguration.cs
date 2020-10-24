using AutoMapper;
using Northwind.Business.Mappers;

namespace Northwind.Web.Mappers
{
    public static class WebMapperConfiguration
    {
        public static IMapperConfigurationExpression ApplyWebMapperConfiguration(
            this IMapperConfigurationExpression configuration)
        {
            configuration
                .ApplyBusinessMapperConfiguration()
                .ApplyCategoryModelMapping()
                .ApplyProductModelMapping()
                .ApplyProductEditModelMapping();

            return configuration;
        }
    }
}
