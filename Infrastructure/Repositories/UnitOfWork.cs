using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<string, object> _repositories = new();
        private IDbContextTransaction? _transaction;
        private bool _disposed = false;
        public DbContext Context => _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_transaction != null)
                return _transaction;

            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_transaction != null)
                    await _transaction.CommitAsync();
            }
            catch
            {
                if (_transaction != null)
                    await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                    await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type))
                return (IGenericRepository<T>)_repositories[type];

            var repo = new GenericRepository<T>(_context);
            _repositories.Add(type, repo);

            return repo;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
            _transaction = null;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Clear repositories
                    _repositories.Clear();
                    // Dispose managed resources
                    _transaction?.Dispose();
                    _context?.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
