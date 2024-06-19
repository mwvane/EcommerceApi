using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class ProductViewCount
    {
        [Key]
        public int ProductViewCountId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime ViewedAt { get; set; }
    }
}
