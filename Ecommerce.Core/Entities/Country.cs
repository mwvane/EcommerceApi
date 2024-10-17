using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.Core.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        [JsonIgnore]
        public ICollection<Manufacturer> Manufacturers { get; set; }
    }
}
