namespace EcommerceApp.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string Manufacturer {  get; set; }
        public double Rating { get; set; }
        public ICollection<string> ProductImages { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public int ViewCount { get; set; }
    }
}
