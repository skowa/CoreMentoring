using AutoMapper;
using Northwind.Business.Mappers;

namespace Northwind.Core.Web.Mappers
{
    public static class CoreWebMapperConfiguration
    {
        public static IMapperConfigurationExpression ApplyCoreWebMapperConfiguration(
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
