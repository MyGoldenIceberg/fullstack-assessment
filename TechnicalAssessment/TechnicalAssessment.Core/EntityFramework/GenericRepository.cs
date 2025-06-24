using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Core.Context;
using TechnicalAssessment.Core.EntityFramework.Interfaces;
using TechnicalAssessment.Core.Model;

namespace TechnicalAssessment.Core.EntityFramework
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Add(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public IQueryable<T> Query() => _dbSet.AsQueryable();
    }
}
