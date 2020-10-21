using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Northwind.Data.Entities;

namespace Northwind.Data.EF.Configuration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<SupplierEntity>
    {
        public void Configure(EntityTypeBuilder<SupplierEntity> builder)
        {
            builder
                .ToTable("Suppliers")
                .HasKey(supplier => supplier.SupplierID);
        }
    }
}
