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
        public async Task<IActionResult> GetManufacturerById(int id)
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
                return Ok(manufacturer);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetManufacturers")]
        public IActionResult GetManufacturers()
        {
            var data = _context.Manufacturers.Include(m => m.Country).Select(m => new ManufacturerDto
            {
                Id = m.ManufacturerId,
                Name = m.Name,
                Country = new CountryDto() { Id = m.Country.CountryId, Name = m.Country.Name, Image = m.Country.Image },
            });
            return Ok(data);
        }
        [HttpPost("AddManufacturer")]
        public async Task<IActionResult> AddManufacturer([FromBody] ManufacturerDto manufacturer)
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
                return Ok(manufacturer);

            }
            catch (Exception ex)
            {
                return BadRequest("Manufacturer not saved," + ex.Message);
            }

        }

        [HttpPut("UpdateManufacturer")]
        public async Task<IActionResult> UpdateManufacturer([FromBody] ManufacturerDto manufacturer)
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
                return Ok(manufacturer);
            }
            catch (Exception ex)
            {
                return BadRequest("Manufacturer not updated," + ex.Message);
            }

        }

        [HttpDelete("DeleteManufacturer")]
        public async Task<IActionResult> DeleteManufacturer([FromBody] List<int> manufacturerIds)
        {
            foreach (var id in manufacturerIds)
            {
                var manufacturer = await _context.Manufacturers.FindAsync(id);
                if (manufacturer != null)
                {
                    _context.Manufacturers.Remove(manufacturer);
                }
            };
            _context.SaveChanges();
            return Ok();

        }
    }
}
