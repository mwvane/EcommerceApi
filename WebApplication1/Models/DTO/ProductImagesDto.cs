namespace EcommerceApp.Models.DTO
{
    public class ProductImagesDto
    {
        public int? Id { get; set; }
        public string? Url { get; set; }
        public IFormCollection? FormFile { get; set; }
        //public Product? Product { get; set; }

    }
}
