using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Data.Entities;

namespace Northwind.Business.Mappers
{
    internal static class CategoryMapping
    {
        internal static IProfileExpression ApplyCategoryMapping(this IProfileExpression configuration)
        {
            return configuration
                .FromCategoryEntity()
                .ToCategoryEntity();
        }

        private static IProfileExpression FromCategoryEntity(this IProfileExpression configuration)
        {
            configuration.CreateMap<CategoryEntity, Category>()
                .ForMember(category => category.CategoryId, opt => opt.MapFrom(categoryEntity => categoryEntity.CategoryID));

            return configuration;
        }

        private static IProfileExpression ToCategoryEntity(this IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategoryEntity>()
                .ForMember(categoryEntity => categoryEntity.CategoryID, opt => opt.MapFrom(category => category.CategoryId));

            return configuration;
        }
    }
}
