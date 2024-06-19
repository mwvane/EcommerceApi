using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace EcommerceApp.Models
{
    public class Manufacturer
    {
        [Key]
        public int ManufacturerId { get; set; }

        [Required]
        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
