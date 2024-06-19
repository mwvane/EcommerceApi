using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<CartItem> Items { get; set; }
    }
}
