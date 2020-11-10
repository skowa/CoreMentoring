using Northwind.Core.Domain;

namespace Northwind.Web.Tests.Unit.Builders
{
    internal class CategoryBuilder
    {
        private readonly Category _category = new Category();

        internal CategoryBuilder WithCategoryName(string categoryName)
        {
            _category.CategoryName = categoryName;

            return this;
        }

        internal Category Build()
        {
            return _category;
        }
    }
}
