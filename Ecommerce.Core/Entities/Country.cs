using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Image { get; set; }

        public ICollection<Manufacturer> Manufacturers { get; set; }
    }
}
