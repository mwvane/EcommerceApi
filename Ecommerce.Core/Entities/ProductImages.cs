using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Ecommerce.Core.Entities
{
    public class ProductImages
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
