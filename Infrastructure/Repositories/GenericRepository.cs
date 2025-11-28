using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = applicationDbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate != null)
                return await _dbSet.CountAsync(predicate);

            return await _dbSet.CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            return true;
        }

        public Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            return query;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(true);
        }
    }
}
