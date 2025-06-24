using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Core.Model;

namespace TechnicalAssessment.Core.EntityFramework.Interfaces
{
    public interface IService<TEntity, TContext> where TEntity : BaseEntity where TContext : DbContext
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Add(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void SoftDelete(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
