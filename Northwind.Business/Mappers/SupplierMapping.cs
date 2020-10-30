using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Data.Entities;

namespace Northwind.Business.Mappers
{
    internal static class SupplierMapping
    {
        internal static IProfileExpression ApplySupplierMapping(this IProfileExpression configuration)
        {
            return configuration
                .FromSupplierEntity()
                .ToSupplierEntity();
        }

        private static IProfileExpression FromSupplierEntity(this IProfileExpression configuration)
        {
            configuration.CreateMap<SupplierEntity, Supplier>()
                .ForMember(supplier => supplier.SupplierId, opt => opt.MapFrom(supplierEntity => supplierEntity.SupplierID));

            return configuration;
        }

        private static IProfileExpression ToSupplierEntity(this IProfileExpression configuration)
        {
            configuration.CreateMap<Supplier, SupplierEntity>()
                .ForMember(supplierEntity => supplierEntity.SupplierID, opt => opt.MapFrom(supplier => supplier.SupplierId));

            return configuration;
        }
    }
}
