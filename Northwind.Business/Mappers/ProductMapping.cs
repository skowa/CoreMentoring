using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Data.Entities;

namespace Northwind.Business.Mappers
{
    internal static class ProductMapping
    {
        internal static IProfileExpression ApplyProductMapping(this IProfileExpression configuration)
        {
            return configuration
                .FromProductEntity()
                .ToProductEntity();
        }

        private static IProfileExpression FromProductEntity(this IProfileExpression configuration)
        {
            configuration.CreateMap<ProductEntity, Product>()
                .ForMember(product => product.ProductId, opt => opt.MapFrom(productEntity => productEntity.ProductID));

            return configuration;
        }

        private static IProfileExpression ToProductEntity(this IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductEntity>()
                .ForMember(productEntity => productEntity.ProductID, opt => opt.MapFrom(product => product.ProductId))
                .ForMember(productEntity => productEntity.CategoryID, opt => opt.MapFrom(product => product.CategoryId));

            return configuration;
        }
    }
}
