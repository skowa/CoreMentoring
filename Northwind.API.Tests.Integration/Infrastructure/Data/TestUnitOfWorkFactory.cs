using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Data.EF;

namespace Northwind.API.Tests.Integration.Infrastructure.Data
{
    public class TestUnitOfWorkFactory : IUnitOfWorkFactory<IUnitOfWork>
    {
        private const string NorthDatabaseName = "Northwind";

        public IUnitOfWork Create()
        {
            var databaseOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(NorthDatabaseName)
                .Options;

            var databaseContext = new TestDbContext(
                databaseOptions,
                builder => {});

            databaseContext.Database.EnsureCreated();

            return new UnitOfWork(databaseContext);
        }
    }
}
