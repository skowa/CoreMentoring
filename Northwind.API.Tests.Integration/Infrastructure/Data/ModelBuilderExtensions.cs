using Microsoft.EntityFrameworkCore;
using Northwind.API.Tests.Integration.Builders;
using Northwind.Data.Entities;

namespace Northwind.API.Tests.Integration.Infrastructure.Data
{
    internal static class ModelBuilderExtensions
    {
        internal static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>()
                .HasKey(category => category.CategoryID);

            modelBuilder.Entity<CategoryEntity>().HasData(
                new CategoryEntityBuilder()
                    .WithCategoryId(InMemoryDbEntitiesValues.TestSeafoodCategoryId)
                    .WithCategoryName(InMemoryDbEntitiesValues.TestSeafoodCategoryName)
                    .WithPicture(InMemoryDbEntitiesValues.TestSeafoodCategoryPicture)
                    .Build(),
                new CategoryEntityBuilder()
                    .WithCategoryId(InMemoryDbEntitiesValues.TestMeatCategoryId)
                    .WithCategoryName(InMemoryDbEntitiesValues.TestMeatCategoryName)
                    .WithPicture(InMemoryDbEntitiesValues.TestMeatCategoryPicture)
                    .Build());

            modelBuilder.Entity<SupplierEntity>()
                .HasKey(supplier => supplier.SupplierID);

            modelBuilder.Entity<SupplierEntity>().HasData(
                new SupplierEntityBuilder()
                    .WithSupplierId(InMemoryDbEntitiesValues.TestSunnyDaySupplierId)
                    .WithCompanyName(InMemoryDbEntitiesValues.TestSunnyDaySupplierName)
                    .Build());

            modelBuilder.Entity<ProductEntity>()
                .HasKey(product => product.ProductID);

            modelBuilder.Entity<ProductEntity>()
                .HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryID);

            modelBuilder.Entity<ProductEntity>()
                .HasOne(product => product.Supplier)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.SupplierID);

            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntityBuilder()
                    .WithProductId(InMemoryDbEntitiesValues.TestTofuProductId)
                    .WithProductName(InMemoryDbEntitiesValues.TestTofuProductName)
                    .WithCategoryId(InMemoryDbEntitiesValues.TestTofuCategoryId)
                    .WithSupplierId(InMemoryDbEntitiesValues.TestTofuSupplierId)
                    .Build(),
                new ProductEntityBuilder()
                    .WithProductId(InMemoryDbEntitiesValues.TestChangProductId)
                    .WithProductName(InMemoryDbEntitiesValues.TestChangProductName)
                    .WithCategoryId(InMemoryDbEntitiesValues.TestChangCategoryId)
                    .WithSupplierId(InMemoryDbEntitiesValues.TestChangSupplierId)
                    .Build());
        }
    }
}
