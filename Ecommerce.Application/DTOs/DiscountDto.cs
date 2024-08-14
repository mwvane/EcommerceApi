namespace EcommerceApp.Models.DTO
{
    public class DiscountDto
    {
        public int Id { get; set; }

        //public Product? Product { get; set; }

        public decimal DiscountAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
