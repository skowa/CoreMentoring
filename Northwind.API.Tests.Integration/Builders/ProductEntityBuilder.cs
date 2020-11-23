using Northwind.Data.Entities;

namespace Northwind.API.Tests.Integration.Builders
{
    internal class ProductEntityBuilder
    {
        private readonly ProductEntity _product = new ProductEntity();

        internal ProductEntityBuilder WithProductId(int id)
        {
            _product.ProductID = id;

            return this;
        }

        internal ProductEntityBuilder WithProductName(string productName)
        {
            _product.ProductName = productName;

            return this;
        }

        internal ProductEntityBuilder WithCategoryId(int categoryId)
        {
            _product.CategoryID = categoryId;

            return this;
        }

        internal ProductEntityBuilder WithSupplierId(int supplierId)
        {
            _product.SupplierID = supplierId;

            return this;
        }

        internal ProductEntity Build()
        {
            return _product;
        }
    }
}
