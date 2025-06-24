using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Core.Model;

namespace TechnicalAssessment.Core.EntityFramework.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        IQueryable<T> Query();
    }
}
