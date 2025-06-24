using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Core.EntityFramework.Interfaces;
using TechnicalAssessment.Core.EntityFramework;

namespace TechnicalAssessment.Core.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSharedInfrastructure<TContext>(this IServiceCollection services)
        where TContext : DbContext
        {
            services.AddScoped(typeof(IUnitOfWork<TContext>), typeof(UnitOfWork<TContext>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IService<,>), typeof(Service<,>));
            return services;
        }
    }
}
