using Northwind.Data.Entities;

namespace Northwind.API.Tests.Integration.Builders
{
    internal class CategoryEntityBuilder
    {
        private readonly CategoryEntity _category = new CategoryEntity();

        internal CategoryEntityBuilder WithCategoryId(int id)
        {
            _category.CategoryID = id;

            return this;
        }

        internal CategoryEntityBuilder WithCategoryName(string categoryName)
        {
            _category.CategoryName = categoryName;

            return this;
        }

        internal CategoryEntityBuilder WithPicture(byte[] picture)
        {
            _category.Picture = picture;

            return this;
        }

        internal CategoryEntity Build()
        {
            return _category;
        }
    }
}
