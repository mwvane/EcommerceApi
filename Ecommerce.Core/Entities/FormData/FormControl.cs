using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities.FormData
{
    public class FormControl
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Placeholder { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        // Foreign Key
        public int SectionId { get; set; }

        // Navigation Properties
        public Section Section { get; set; } = null!;
        public ICollection<Validator> Validators { get; set; } = new List<Validator>();
        public ICollection<AdditionalLink> AdditionalLinks { get; set; } = new List<AdditionalLink>();
        [AllowNull]
        public ICollection<FormOption> Options { get; set; } = new List<FormOption>();
    }
}
