using Microsoft.EntityFrameworkCore;
using Northwind.Data.EF.Configuration;

namespace Northwind.Data.EF
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory<IUnitOfWork>
    {
        private readonly string _connectionString;

        public UnitOfWorkFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IUnitOfWork Create()
        {
            var databaseOptions = new DbContextOptionsBuilder<ConfigurableDbContext>()
                .UseSqlServer(_connectionString)
                .Options;

            var databaseContext = new ConfigurableDbContext(
                databaseOptions,
                builder =>
                {
                    builder.ApplyConfiguration(new CategoryConfiguration());
                    builder.ApplyConfiguration(new SupplierConfiguration());
                    builder.ApplyConfiguration(new ProductConfiguration());
                });

            return new UnitOfWork(databaseContext);
        }
    }
}
