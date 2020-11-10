using Northwind.Web.Models;

namespace Northwind.Web.Tests.Unit.Builders
{
    internal class CategoryModelBuilder
    {
        private readonly CategoryModel _categoryModel = new CategoryModel();

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
