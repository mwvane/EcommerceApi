using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Ecommerce.Core.Entities.FormData
{
    public class Form
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ResetButtonTitle { get; set; } = string.Empty;
        public string SubmitButtonTitle { get; set; } = string.Empty;
        public string SubmitButtonIcon { get; set; } = string.Empty;
        public string ResetButtonIcon { get; set; } = string.Empty;
        public bool Loading { get; set; }

        // Navigation Property
        public ICollection<Section> Sections { get; set; } = new List<Section>();
    }
}
