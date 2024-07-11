namespace EcommerceApp.Models.DTO
{
    public class OptionDto
    {
        public int OptionId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int OptionTypeId {  get; set; }
        public OptionType? OptionType { get; set; }
        //public ICollection<ProductOption>? ProductOptions { get; set; }
    }
}
