using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ITIContext context;

        public Repository(ITIContext context)
        {
            this.context = context;
        }

        public async Task InsertAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            List<T> results = await query.ToListAsync();

            return results;
        }

        public async Task<T?> GetByIdAsync(
            Expression<Func<T, bool>> filter,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            T? result = await query.FirstOrDefaultAsync(filter);

            return result;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}
