using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }

        public DateTime Timestamp { get; set; }

        public string Message { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
