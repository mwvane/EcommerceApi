using EcommerceApp.Interfaces;

namespace EcommerceApp.Models.DTO
{
    public class OptionTypeDto : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Option>? Options { get; set; }
    }
}
