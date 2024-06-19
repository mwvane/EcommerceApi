using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
