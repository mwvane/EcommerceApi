using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.Entities
{
    public class WishlistItem
    {
        [Key]
        public int Id { get; set; }

        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
