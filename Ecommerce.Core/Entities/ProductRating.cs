using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.Entities
{
    public class ProductRating
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int Rating { get; set; }

        public string Review { get; set; }
    }
}
