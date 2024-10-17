using System.Text.Json.Serialization;

namespace Ecommerce.Core.Entities
{
    public class OptionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Option> Options { get; set; }
    }
}
