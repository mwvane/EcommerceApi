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
                return new Result() { Error = new List<string> { $"manufacturer with id {id} not found" } };
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
            if(manufacturer != null)
            {
                return new Result()
                {
                    Data = manufacturer,
                    Success = "true"
                };
            }
            return new Result() { Error = new List<string> { "manufacturers couldn't load" } };
        }
        [HttpPost("AddManufacturer")]
        public async Task<Result> AddManufacturer([FromBody] ManufacturerDto manufacturer)
        {
            var newManufacturer = new Manufacturer()
            {
                CountryId = manufacturer.Country.Id,
                Name = manufacturer.Name
            };
            try
            {
                await _context.Manufacturers.AddAsync(newManufacturer);
                await _context.SaveChangesAsync();
                return new Result()
                {
                    Data = newManufacturer,
                    Success = "manufacturer successfully added"
                };

            }
            catch (Exception ex)
            {
                return new Result() { Error = new List<string>() { "faild to add," + ex.Message } };
            }

        }

        [HttpPut("UpdateManufacturer")]
        public async Task<Result> UpdateManufacturer([FromBody] ManufacturerDto manufacturer)
        {
            var newManufacturer = new Manufacturer()
            {

                ManufacturerId = manufacturer.Id,
                CountryId = manufacturer.Country.Id,
                Name = manufacturer.Name
            };
            try
            {
                _context.Manufacturers.Update(newManufacturer);
                await _context.SaveChangesAsync();
                return new Result()
                {
                    Data = newManufacturer,
                    Success = "manufacturer successfully updated"
                };
            }
            catch (Exception ex)
            {
                return new Result() { Error = new List<string>() { "faild to update," + ex.Message } };
            }

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
            };
            try
            {
                await _context.SaveChangesAsync();
                return new Result()
                {
                    Success = "manufacturer successfully updated"
                };
            }
            catch (Exception ex)
            {
                return new Result() { Error = new List<string>() { "faild to delete", ex.Message } };

            }
        }
    }
}
