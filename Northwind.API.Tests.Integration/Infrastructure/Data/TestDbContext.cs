using System;
using Microsoft.EntityFrameworkCore;
using Northwind.Data.EF;

namespace Northwind.API.Tests.Integration.Infrastructure.Data
{
    internal class TestDbContext : ConfigurableDbContext
    {
        internal TestDbContext(DbContextOptions options, Action<ModelBuilder> modelBuilderConfigurator)
            : base(options, modelBuilderConfigurator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
