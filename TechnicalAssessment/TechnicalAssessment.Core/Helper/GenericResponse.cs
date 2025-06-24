using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssessment.Core.Helper
{
    public class GenericResponse<T>
    {
        public T? Payload { get; set; }
        public string? ErrorMessage { get; set; }
        public int? TotalCount { get; set; }
        public bool Success => string.IsNullOrEmpty(ErrorMessage);
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
