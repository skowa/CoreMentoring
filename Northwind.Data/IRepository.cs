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

        IQueryable<T> Get(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAsync();

        IQueryable<T> GetWith(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> GetWith(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetWithAsync(params Expression<Func<T, object>>[] includeProperties);

        T Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
