using AutoMapper;

namespace Northwind.Web.Mappers
{
    public static class WebMapperConfiguration
    {
        public static IMapperConfigurationExpression ApplyWebMapperConfiguration(
            this IMapperConfigurationExpression configuration)
        {
            configuration
                .ApplyCoreWebMapperConfiguration()
                .ApplyProductEditModelMapping();

            return configuration;
        }
    }
}
