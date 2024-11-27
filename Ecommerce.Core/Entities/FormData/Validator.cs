using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities.FormData
{
    public class Validator
    {
        public int Id { get; set; }
        public string ValidatorName { get; set; } = string.Empty;
        public bool Required { get; set; }
        public string Message { get; set; } = string.Empty;

        // Foreign Key
        public int FormControlId { get; set; }

        // Navigation Property
        public FormControl FormControl { get; set; } = null!;
    }
}
