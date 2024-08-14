using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Ecommerce.Core.Entities
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
