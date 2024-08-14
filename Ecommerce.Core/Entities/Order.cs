using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
