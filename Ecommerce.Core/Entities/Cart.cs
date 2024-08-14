using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<CartItem> Items { get; set; }
    }
}
