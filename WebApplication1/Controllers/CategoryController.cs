using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly Context _context;
        public CategoryController(Context context)
        {
            _context = context;
        }
        [HttpGet("GetCategories")]
        public Result  GetCategories()
        {
            var data = _context.Categories.Include(c => c.SubCategories).ToList().Where(x => x.ParentCategoryId == null);
            return new Result() { Data = data };
        }

        [HttpGet("GetAllCategories")]
        public Result GetAllCategories()
        {
            var data = _context.Categories.Select(x => new CategoryDto
            {
                Id = x.CategoryId,
                Name = x.Name,
                Image = x.Image,
            });
            return new Result() { Data = data };
        }

        [HttpPost("AddCategory")]
        public Result AddCategory([FromBody] CategoryDto category)
        {
            var exist = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (!exist)
            {
                _context.Categories.Add(new Category { Name = category.Name, Image = category.Image});
                _context.SaveChanges();
                return new Result { Data = category };
            }
            return new Result { Error = new List<string> { "category with this name, already exist" } };
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] List<int> categoryIds)
        {
            foreach (var id in categoryIds)
            {
                var category = await _context.Categories.FindAsync(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                }
            };
            _context.SaveChanges();
            return Ok();

        }

    }
}
