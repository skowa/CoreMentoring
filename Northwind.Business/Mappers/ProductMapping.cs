using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Data.Entities;

namespace Northwind.Business.Mappers
{
    internal static class ProductMapping
    {
        internal static IProfileExpression ApplyProductMapping(this IProfileExpression configuration)
        {
            return configuration.FromProductEntity();
        }

        private static IProfileExpression FromProductEntity(this IProfileExpression configuration)
        {
            configuration.CreateMap<ProductEntity, Product>();

            return configuration;
        }
    }
}
