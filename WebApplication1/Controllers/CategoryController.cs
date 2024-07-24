using EcommerceApp.Data;
using EcommerceApp.ErrorHandling;
using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("GetCategoryById/{id}")]
        public async Task<Result> GetCategoryById(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);
            if(category != null)
            {
                return new Result()
                {
                    Data = category
                };
            }
            else
            {
                throw new NotFoundException($"category with ID - {id} not found ");
            }
        }

        [HttpGet("GetCategories")]
        public Result GetCategories()
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

        [Authorize]
        [HttpPost("AddCategory")]
        public async Task<Result> AddCategory([FromBody] CategoryDto category, CRUD_Action action = CRUD_Action.Create)
        {
            var exist = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (!exist)
            {
                try
                {
                    Category newCategory = new Category { CategoryId = category.Id,  Name = category.Name, Image = category.Image, ParentCategoryId = category.ParentCategoryId };

                    if (action == CRUD_Action.Create)
                    {
                        await _context.Categories.AddAsync(newCategory);
                    }
                    else if(action == CRUD_Action.Update)
                    {
                        _context.Categories.Update(newCategory);
                    }
                    await _context.SaveChangesAsync();
                    return new Result()
                    {
                        Data = newCategory,
                        Notification = new Notification()
                        {
                            Message = $"category with name '{category.Name.ToUpper()}' {action.ToString()}d successfully",
                            Status = NotificationStatus.Success,
                            Title = $"successfully {action.ToString()}d"
                        } 
                    };
                }
                catch (Exception ex)
                {
                    throw new NotFoundException($"faild to {action.ToString()} category");
                }
            }
            else
            {
                return new Result()
                {
                    Notification = new Notification()
                    {
                        Message = $"category with name '{category.Name}' already exist",
                        Status = NotificationStatus.Warning,
                        Title = $"already exist"
                    }
                };
            }
        }
        [HttpPut("UpdateCategory")]
        public async Task<Result> UpdateCategory([FromBody] CategoryDto category)
        {
            var result = await AddCategory(category, CRUD_Action.Update);
            return result;
        }

        [HttpDelete("DeleteCategory")]
        public async Task<Result> DeleteCategory([FromBody] List<int> categoryIds)
        {
            foreach (var id in categoryIds)
            {
                var category = await _context.Categories.FindAsync(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                }
                else
                {
                    return new Result()
                    {
                        Notification = new Notification()
                        {
                            Message = $"failed:  category with id = {id} not found",
                            Status = NotificationStatus.Error,
                            Title = "couldn't deleted"
                        }
                    };
                };
            }
            try
            {
                await _context.SaveChangesAsync();
                return new Result()
                {
                    Notification = new Notification()
                    {
                        Message = "selected categories deleted successfully",
                        Status = NotificationStatus.Success,
                        Title = "successfully deleted"
                    }
                };

            }
            catch (Exception ex)
            {
                throw new NotFoundException("Failed to deletete selected categories");
            }

        }

    }
}
