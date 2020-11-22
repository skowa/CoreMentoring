using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Data.EF
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return _dbSet;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter)
        {
            return Get().Where(filter);
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await Get().ToListAsync();
        }

        public IQueryable<T> GetWith(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Get();
            if (includeProperties == null || includeProperties.Length == 0)
            {
                return query;
            }

            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task<IEnumerable<T>> GetWithAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            return await GetWith(includeProperties).ToListAsync();
        }

        public T Add(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
