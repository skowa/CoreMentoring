using Northwind.Core.Domain;

namespace Northwind.Web.Tests.Unit.Builders
{
    internal class ProductBuilder
    {
        private readonly Product _product = new Product();

        internal ProductBuilder WithProductName(string productName)
        {
            _product.ProductName = productName;

            return this;
        }

        internal Product Build()
        {
            return _product;
        }
    }
}
