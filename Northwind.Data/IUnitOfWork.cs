using System.Threading.Tasks;

namespace Northwind.Data
{
    public interface IUnitOfWork
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        IRepository<T> Repository<T>()
            where T : class;
    }
}
