using System;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Data.EF
{
    public class ConfigurableDbContext : DbContext
    {
        private const string DefaultDatabaseSchema = "dbo";

        public ConfigurableDbContext(DbContextOptions options, Action<ModelBuilder> modelBuilderConfigurator)
            : base(options)
        {
            ModelBuilderConfigurator = modelBuilderConfigurator;
        }

        public Action<ModelBuilder> ModelBuilderConfigurator { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultDatabaseSchema);
            ModelBuilderConfigurator(modelBuilder);
        }
    }
}
