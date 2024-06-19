using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<WishlistItem> Items { get; set; }
    }
}
