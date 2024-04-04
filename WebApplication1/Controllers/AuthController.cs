using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Models;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Context _context;
        public AuthController(Context context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public Result Login(string payload)
        {
            return new Result() { Error = new List<string>() { "test" } };
        }
    }
}
