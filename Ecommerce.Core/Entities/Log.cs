using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.Entities
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Message { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
