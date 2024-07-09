using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly Context _context;
        public CountryController(Context context)
        {
            _context = context;
        }
        [HttpGet("GetCountries")]
        public IActionResult GetCountries()
        {
            var data =  _context.Countries.Select(c => new CountryDto()
            {
                Id = c.CountryId,
                Name = c.Name,
                Image = c.Image
            });
            return Ok(data);
        }

        [HttpDelete("DeleteCountry")]
        public async Task<IActionResult> DeleteCountry([FromBody] List<int> countryIds)
        {
            foreach (var id in countryIds)
            {
                var country = await _context.Countries.FindAsync(id);
                if (country != null)
                {
                    _context.Countries.Remove(country);
                }
                else
                {
                    return NotFound($"country with id = {id} not found");
                }
            };
            _context.SaveChanges();
            return Ok("country successfully deleted");

        }
    }
}
