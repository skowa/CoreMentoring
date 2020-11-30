using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Core.Web.Models;

namespace Northwind.Core.Web.Mappers
{
    internal static class CategoryModelMapping
    {
        internal static IProfileExpression ApplyCategoryModelMapping(this IProfileExpression configuration)
        {
            return configuration.FromCategory();
        }

        private static IProfileExpression FromCategory(this IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategoryModel>();

            return configuration;
        }
    }
}
