using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Data.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext dbContext)
        {
            _context = dbContext;
        }

        public IRepository<T> Repository<T>()
            where T : class
        {
            return new Repository<T>(_context);
        }

        public int SaveChanges()
        {
            _context.ChangeTracker.DetectChanges();

            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            _context.ChangeTracker.DetectChanges();

            return _context.SaveChangesAsync();
        }
    }
}
