using System.Linq.Expressions;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task InsertAsync(T entity);
        Task<List<T>> GetAllAsync(
         Expression<Func<T, bool>>? filter = null,
         params Expression<Func<T, object>>[] includes);

        Task<T?> GetByIdAsync(
            Expression<Func<T, bool>> filter,
            params Expression<Func<T, object>>[] includes);
        void Delete(T entity);
    }
}
