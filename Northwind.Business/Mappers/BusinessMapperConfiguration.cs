using AutoMapper;

namespace Northwind.Business.Mappers
{
    public static class BusinessMapperConfiguration
    {
        public static IMapperConfigurationExpression ApplyBusinessMapperConfiguration(
            this IMapperConfigurationExpression configuration)
        {
            configuration
                .ApplyCategoryMapping()
                .ApplySupplierMapping()
                .ApplyProductMapping()
                .ApplyUserMapping();

            return configuration;
        }
    }
}
