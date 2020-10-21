using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Data.Entities;

namespace Northwind.Business.Mappers
{
    internal static class CategoryMapping
    {
        internal static IProfileExpression ApplyCategoryMapping(this IProfileExpression configuration)
        {
            return configuration.FromCategoryEntity();
        }

        private static IProfileExpression FromCategoryEntity(this IProfileExpression configuration)
        {
            configuration.CreateMap<CategoryEntity, Category>();

            return configuration;
        }
    }
}
