using EcommerceApp.Models.DTO;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : Controller
    {
        private readonly Context _context;
        public OptionController(Context context)
        {
            _context = context;
        }
        [HttpGet("GetOptions")]
        public Result GetOptions()
        {
            var data = _context.Options.Select(o => new OptionDto
            {
                Id = o.OptionId,
                Name = $"{o.Name} ({o.Value} )",
                Value = o.Value,
                OptionType = o.OptionType
        });
            return new Result() { Data = data };
        }
    }
}
