using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public string Phone { get; set; }
        public string? Image { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        public UserRole Role { get; set; } = UserRole.Client;

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
