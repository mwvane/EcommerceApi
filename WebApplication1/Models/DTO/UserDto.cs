using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string? Image { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public UserRole Role { get; set; }
    }
}
