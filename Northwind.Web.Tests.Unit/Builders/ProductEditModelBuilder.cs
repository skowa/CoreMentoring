using Northwind.Core.Web.Models;

namespace Northwind.Web.Tests.Unit.Builders
{
    internal class ProductEditModelBuilder
    {
        private readonly ProductEditModel _productModel = new ProductEditModel();

        internal ProductEditModelBuilder WithProductName(string productName)
        {
            _productModel.ProductName = productName;

            return this;
        }

        internal ProductEditModel Build()
        {
            return _productModel;
        }
    }
}
