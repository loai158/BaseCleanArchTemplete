using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> Get(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = true);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = true);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    }
}
