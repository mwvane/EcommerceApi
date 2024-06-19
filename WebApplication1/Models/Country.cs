using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Manufacturer> Manufacturers { get; set; }
    }
}
