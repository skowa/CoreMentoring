using Northwind.Data.Entities;

namespace Northwind.API.Tests.Integration.Builders
{
    internal class SupplierEntityBuilder
    {
        private readonly SupplierEntity _supplier = new SupplierEntity();

        internal SupplierEntityBuilder WithSupplierId(int id)
        {
            _supplier.SupplierID = id;

            return this;
        }

        internal SupplierEntityBuilder WithCompanyName(string companyName)
        {
            _supplier.CompanyName = companyName;

            return this;
        }

        internal SupplierEntity Build()
        {
            return _supplier;
        }
    }
}
