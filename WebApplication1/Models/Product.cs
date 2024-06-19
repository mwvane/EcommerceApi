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
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public ICollection<ProductImages> ProductImages { get; set; }

        public ICollection<ProductRating> Ratings { get; set; }

        public ICollection<ProductViewCount> ViewCounts { get; set; }

        public ICollection<Discount> Discounts { get; set; }    

    }
}
