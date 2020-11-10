using Northwind.Core.Domain;

namespace Northwind.Web.Tests.Unit.Builders
{
    internal class SupplierBuilder
    {
        private readonly Supplier _supplier = new Supplier();

        internal SupplierBuilder WithCompanyName(string companyName)
        {
            _supplier.CompanyName = companyName;

            return this;
        }

        internal Supplier Build()
        {
            return _supplier;
        }
    }
}
