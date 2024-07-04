namespace EcommerceApp.Models.DTO
{
    public class OptionTypeDto
    {
        public int OptionTypeId { get; set; }
        public string Name { get; set; }
        public ICollection<Option>? Options { get; set; }
    }
}
