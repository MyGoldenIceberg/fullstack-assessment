using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Core.EntityFramework.Interfaces;
using TechnicalAssessment.Core.Model;
using TechnicalAssessment.Core.Context;

namespace TechnicalAssessment.Core.EntityFramework
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private IDbContextTransaction? _transaction;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new GenericRepository<TEntity>(_context);
            }
            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public async Task BeginTransactionAsync()
        {
            if (_transaction is null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction is not null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
