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
        [HttpGet("GetManufacturers")]
        public Result GetManufacturers()
        {
            var data = _context.Manufacturers.Include(m => m.Country).Select(m => new ManufacturerDto
            {
                Id = m.ManufacturerId, 
                Name = m.Name,
                CountryName = m.Country.Name
            });
            return new Result() { Data = data };
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
