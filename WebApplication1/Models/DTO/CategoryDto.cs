namespace EcommerceApp.Models.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int? ParentCategoryId {  get; set; } 
        //public Category? ParentCategory{  get; set; }
        //public ICollection<Category>? SubCategories { get; set; }
        public string? Image { get; set; }        
    }
}
