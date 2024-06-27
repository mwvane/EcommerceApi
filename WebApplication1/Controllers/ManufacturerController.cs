using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;

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
            var data = _context.Manufacturers.Select(m => new ManufacturerDto
            {
                Id = m.ManufacturerId, 
                Name = m.Name
            });
            return new Result() { Data = data };
        }
    }
}
