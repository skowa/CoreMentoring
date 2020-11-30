using Northwind.API.Tests.Integration.Infrastructure.Services.Models;

namespace Northwind.API.Tests.Integration.Builders
{
    internal class ProductModelBuilder
    {
        private readonly ProductModel _productModel = new ProductModel();

        internal ProductModelBuilder WithProductId(int productId)
        {
            _productModel.ProductId = productId;

            return this;
        }

        internal ProductModelBuilder WithProductName(string productName)
        {
            _productModel.ProductName = productName;

            return this;
        }

        internal ProductModelBuilder WithCategoryName(string categoryName)
        {
            _productModel.CategoryName = categoryName;

            return this;
        }

        internal ProductModelBuilder WithSupplierName(string supplierName)
        {
            _productModel.SupplierName = supplierName;

            return this;
        }

        internal ProductModel Build()
        {
            return _productModel;
        }
    }
}
