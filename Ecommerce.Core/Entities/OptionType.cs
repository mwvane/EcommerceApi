namespace Ecommerce.Core.Entities
{
    public class OptionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
