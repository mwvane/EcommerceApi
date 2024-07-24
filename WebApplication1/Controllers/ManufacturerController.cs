using EcommerceApp.Data;
using EcommerceApp.ErrorHandling;
using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : Controller
    {
        private readonly Context _context;
        public ManufacturerController(Context context)
        {
            _context = context;
        }
        [HttpGet("GetManufacturerById/{id}")]
        public async Task<Result> GetManufacturerById(int id)
        {
            var manufacturer = await _context.Manufacturers.
                Include(c => c.Country).
                Select(m => new ManufacturerDto
                {
                    Id = m.ManufacturerId,
                    Name = m.Name,
                    Country = new CountryDto
                    {
                        Id = m.Country.CountryId,
                        Image = m.Country.Image,
                        Name = m.Country.Name
                    }
                }).SingleOrDefaultAsync(c => c.Id == id);
            if (manufacturer != null)
            {
                return new Result()
                {
                    Data = manufacturer,
                    Success = "true"
                };
            }
            else
            {
                throw new NotFoundException($"manufacturer with id {id} not found");
            }
        }

        [HttpGet("GetManufacturers")]
        public Result GetManufacturers()
        {
            var manufacturer = _context.Manufacturers.Include(m => m.Country).Select(m => new ManufacturerDto
            {
                Id = m.ManufacturerId,
                Name = m.Name,
                Country = new CountryDto() { Id = m.Country.CountryId, Name = m.Country.Name, Image = m.Country.Image },
            });
            if (manufacturer != null)
            {
                return new Result()
                {
                    Data = manufacturer,
                    Success = "true"
                };
            }
            throw new NotFoundException("manufacturers couldn't load");
        }

        [HttpPost("AddManufacturer")]
        public async Task<Result> AddManufacturer([FromBody] ManufacturerDto manufacturer, CRUD_Action action = CRUD_Action.Create)
        {
            if (_context.Manufacturers.Any(m => m.Name == manufacturer.Name))
            {
                return new Result()
                {
                    Notification = new Notification()
                    {
                        Message = $"manufacturer with name '{manufacturer.Name}' already exists",
                        Status = NotificationStatus.Warning,
                        Title = "already exists"
                    }
                };
            }
            var newManufacturer = new Manufacturer()
            {
                ManufacturerId = manufacturer.Id,
                CountryId = manufacturer.Country.Id,
                Name = manufacturer.Name
            };
            try
            {
                if (action == CRUD_Action.Create)
                {
                    await _context.Manufacturers.AddAsync(newManufacturer);
                }
                else if (action == CRUD_Action.Update)
                {
                    _context.Manufacturers.Update(newManufacturer);
                }
                await _context.SaveChangesAsync();
                return new Result()
                {
                    Data = newManufacturer,
                    Notification = new Notification()
                    {
                        Message = $"manufacturer with name '{manufacturer.Name.ToUpper()}' {action.ToString()}d successfully",
                        Title = "successfully added",
                        Status = NotificationStatus.Success,
                    }
                };

            }
            catch (Exception ex)
            {
                return new Result() {Notification = new Notification() { 
                    Message  = $"faild to {action.ToString()} manufacturer",
                    Status = NotificationStatus.Error,
                    Title = "Error",
                } };
            }

        }

        [HttpPut("UpdateManufacturer")]
        public async Task<Result> UpdateManufacturer([FromBody] ManufacturerDto manufacturer)
        {
            var result =  await AddManufacturer(manufacturer, CRUD_Action.Update);
            return result;

        }

        [HttpDelete("DeleteManufacturer")]
        public async Task<Result> DeleteManufacturer([FromBody] List<int> manufacturerIds)
        {
            foreach (var id in manufacturerIds)
            {
                var manufacturer = await _context.Manufacturers.FindAsync(id);
                if (manufacturer != null)
                {
                    _context.Manufacturers.Remove(manufacturer);
                }
                else
                {
                    return new Result()
                    {
                        Notification = new Notification()
                        {
                            Message = $"slected manufacturer couldn't deleted . manufacturer with id = {id} not found",
                            Status = NotificationStatus.Error,
                            Title = "Couldn't delete"
                        },
                    };
                }
            };
            try
            {
                await _context.SaveChangesAsync();
                return new Result()
                {
                    Notification = new Notification()
                    {
                        Message = $"manufacturer deleted successfully",
                        Status = NotificationStatus.Success,
                        Title = "successfully deleted"
                    },
                };
            }
            catch (Exception ex)
            {
                throw new Exception("somthing went wrong! : " + ex.Message);
            }
        }
    }
}
