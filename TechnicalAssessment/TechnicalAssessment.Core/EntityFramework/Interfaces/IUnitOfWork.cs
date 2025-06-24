using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Core.Model;

namespace TechnicalAssessment.Core.EntityFramework.Interfaces
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        Task<int> SaveAsync();
    }
}
