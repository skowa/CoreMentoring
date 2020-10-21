using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Data.Entities;

namespace Northwind.Business.Mappers
{
    internal static class SupplierMapping
    {
        internal static IProfileExpression ApplySupplierMapping(this IProfileExpression configuration)
        {
            return configuration.FromSupplierEntity();
        }

        private static IProfileExpression FromSupplierEntity(this IProfileExpression configuration)
        {
            configuration.CreateMap<SupplierEntity, Supplier>();

            return configuration;
        }
    }
}
