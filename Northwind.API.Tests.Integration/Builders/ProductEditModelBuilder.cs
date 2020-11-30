using Northwind.API.Tests.Integration.Infrastructure.Services.Models;

namespace Northwind.API.Tests.Integration.Builders
{
    internal class ProductEditModelBuilder
    {
        private readonly ProductEditModel _productModel = new ProductEditModel();

        internal ProductEditModelBuilder WithProductId(int productId)
        {
            _productModel.ProductId = productId;

            return this;
        }

        internal ProductEditModelBuilder WithProductName(string productName)
        {
            _productModel.ProductName = productName;

            return this;
        }

        internal ProductEditModelBuilder WithCategoryId(int categoryId)
        {
            _productModel.CategoryId = categoryId;

            return this;
        }

        internal ProductEditModelBuilder WithSupplierId(int supplierId)
        {
            _productModel.SupplierId = supplierId;

            return this;
        }

        internal ProductEditModel Build()
        {
            return _productModel;
        }
    }
}
