using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Web.Models;

namespace Northwind.Web.Mappers
{
    internal static class ProductModelMapping
    {
        internal static IProfileExpression ApplyProductModelMapping(this IProfileExpression configuration)
        {
            return configuration.FromProduct();
        }

        private static IProfileExpression FromProduct(this IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductModel>()
                .ForMember(productModel => productModel.CategoryName, opt => opt.MapFrom(product => product.Category.CategoryName))
                .ForMember(productModel => productModel.SupplierName, opt => opt.MapFrom(product => product.Supplier.CompanyName));

            return configuration;
        }
    }
}
