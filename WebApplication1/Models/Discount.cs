using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EcommerceApp.Models
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal DiscountAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
