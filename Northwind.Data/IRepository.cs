using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Northwind.Data
{
    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> Get();

        Task<IEnumerable<T>> GetAsync();

        IQueryable<T> GetWith(params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetWithAsync(params Expression<Func<T, object>>[] includeProperties);
    }
}
