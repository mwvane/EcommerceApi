namespace EcommerceApp.Models
{
    public class OptionType
    {
        public int OptionTypeId { get; set; }
        public string Name { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
