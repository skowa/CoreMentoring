using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Northwind.Data.Entities;

namespace Northwind.Data.EF.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder
                .ToTable("Products")
                .HasKey(product => product.ProductID);

            builder
                .HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryID);

            builder
                .HasOne(product => product.Supplier)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.SupplierID);
        }
    }
}
