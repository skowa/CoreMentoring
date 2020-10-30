using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Web.Models;

namespace Northwind.Web.Mappers
{
    internal static class ProductEditModelMapping
    {
        internal static IProfileExpression ApplyProductEditModelMapping(this IProfileExpression configuration)
        {
            return configuration
                .FromProduct()
                .ToProduct();
        }

        private static IProfileExpression ToProduct(this IProfileExpression configuration)
        {
            configuration.CreateMap<ProductEditModel, Product>();

            return configuration;
        }

        private static IProfileExpression FromProduct(this IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductEditModel>();

            return configuration;
        }
    }
}
