using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Core.Model;

namespace TechnicalAssessment.Core.Context
{
    public class AppDbContext : DbContext
    {
        protected AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product { Id = Guid.Parse("171c8825-c739-46a4-b66d-2bd7c2b5f056"), Name = "Laptop", Description = "Gaming Laptop", Price = 1500, CategoryId = Guid.Parse("809e7a3c-2895-41ea-9961-f375b12a1d41") },
                new Product { Id = Guid.Parse("847274a4-8b0e-44a0-9c0e-516e73be4468"), Name = "Smartphone", Description = "Latest Android", Price = 800, CategoryId = Guid.Parse("9e45b88a-6811-4d94-b00f-57e74d5a15ba") }
            );

            base.OnModelCreating(builder);
            ApplySoftDeleteFilter(builder);
        }

        private void ApplySoftDeleteFilter(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(AppDbContext)
                        .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                        .MakeGenericMethod(entityType.ClrType);

                    method.Invoke(null, new object[] { builder });
                }
            }
        }

        private static void SetSoftDeleteFilter<TEntity>(ModelBuilder builder) where TEntity : BaseEntity
        {
            builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }

    }
}
