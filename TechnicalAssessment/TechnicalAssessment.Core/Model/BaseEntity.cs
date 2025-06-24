using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssessment.Core.Model
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}