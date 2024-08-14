using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.Entities
{
    public class ProductViewCount
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime ViewedAt { get; set; }
    }
}
