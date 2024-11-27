using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities.FormData
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Border { get; set; }

        // Foreign Key
        public int FormId { get; set; }

        // Navigation Properties
        public Form Form { get; set; } = null!;
        public ICollection<FormControl> FormControls { get; set; } = new List<FormControl>();
    }
}
