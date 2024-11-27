using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities.FormData
{
    public class FormOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        // Foreign Key
        public int FormControlId { get; set; }

        // Navigation Property
        public FormControl FormControl { get; set; } = null!;
    }
}
