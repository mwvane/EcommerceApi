namespace Ecommerce.Core.Entities
{
    public class ProductOption
    {
        public int Id { get; set; }
        public Product Product { get; set; }

        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}
