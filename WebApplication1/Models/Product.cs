using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EcommerceApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public int ManufacturerId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public ICollection<ProductOption> ProductOptions { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public ICollection<ProductImages> ProductImages { get; set; }

        public ICollection<ProductRating> Ratings { get; set; }

        public ICollection<ProductViewCount> ViewCounts { get; set; }

        public ICollection<Discount> Discounts { get; set; }

    }
}
