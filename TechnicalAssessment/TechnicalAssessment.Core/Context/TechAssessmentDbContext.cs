using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Core.Model;

namespace TechnicalAssessment.Core.Context
{
    public class TechAssessmentDbContext : AppDbContext
    {
        public TechAssessmentDbContext(DbContextOptions<TechAssessmentDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
