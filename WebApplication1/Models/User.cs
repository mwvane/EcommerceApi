using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EcommerceApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, NotNull]
        public string Firstname { get; set; }
        public string Lasttname { get; set; }
        [Required, NotNull]
        public string Email { get; set; }
        [Required, NotNull]
        public string Password { get; set; }
        public Boolean IsDeleted { get; set; } = false;
        [Required, NotNull]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
