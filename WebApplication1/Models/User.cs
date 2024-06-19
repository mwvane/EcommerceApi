using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EcommerceApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        public ICollection<Cart> Carts { get; set; }

        public ICollection<Wishlist> Wishlists { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
    public enum UserRole
    {
        Client,
        Admin,
        Manager,
    }
}
