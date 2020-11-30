using Northwind.Core.Web.Models;
using Northwind.Web.Models;

namespace Northwind.Web.Tests.Unit.Builders
{
    internal class ProductModelBuilder
    {
        private readonly ProductModel _productModel = new ProductModel();

        internal ProductModelBuilder WithProductName(string productName)
        {
            _productModel.ProductName = productName;

            return this;
        }

        internal ProductModel Build()
        {
            return _productModel;
        }
    }
}
