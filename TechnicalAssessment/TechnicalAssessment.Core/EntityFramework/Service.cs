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
    public class Service<TEntity, TContext> : IService<TEntity, TContext> where TEntity : BaseEntity where TContext : DbContext
    {
        protected readonly IUnitOfWork<TContext> _unitOfWork;
        protected readonly IGenericRepository<TEntity> _repository;

        public Service(IUnitOfWork<TContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task AddAsync(TEntity entity) => await _repository.AddAsync(entity);

        public void Add(TEntity entity) => _repository.Add(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                await _repository.AddAsync(entity);
        }

        public void Update(TEntity entity) => _repository.Update(entity);

        public void Delete(TEntity entity) => _repository.Delete(entity);

        public void SoftDelete(TEntity entity) => _repository.SoftDelete(entity);

        public async Task<int> SaveChangesAsync() => await _unitOfWork.SaveAsync();
    }
}
