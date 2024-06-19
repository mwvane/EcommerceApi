using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class CategoryDto
    {
        public int? CategoryId { get; set; }

        public string Name { get; set; }
        public string? Icon {  get; set; }

        public int? ParentCategoryId { get; set; }

    }
}
