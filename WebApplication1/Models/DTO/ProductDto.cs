namespace EcommerceApp.Models.DTO
{
    public class ProductDto
    {
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public CategoryDto Category { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public ManufacturerDto Manufacturer { get; set; }
        public double? Rating { get; set; } = 0;
        public ProductImagesDto? ProductImages { get; set; }
        public ICollection<DiscountDto>?  Discounts { get; set; }
        public int? ViewCount { get; set; } = 0;
        //public ICollection<ProductOption>? ProductOptions { get; set; }
        public ICollection<OptionDto> Options { get; set; }
    }
}
