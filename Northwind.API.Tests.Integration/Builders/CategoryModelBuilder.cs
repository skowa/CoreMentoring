using Northwind.API.Tests.Integration.Infrastructure.Services.Models;

namespace Northwind.API.Tests.Integration.Builders
{
    internal class CategoryModelBuilder
    {
        private readonly CategoryModel _categoryModel = new CategoryModel();

        internal CategoryModelBuilder WithCategoryId(int id)
        {
            _categoryModel.CategoryId = id;

            return this;
        }

        internal CategoryModelBuilder WithCategoryName(string categoryName)
        {
            _categoryModel.CategoryName = categoryName;

            return this;
        }

        internal CategoryModel Build()
        {
            return _categoryModel;
        }
    }
}
