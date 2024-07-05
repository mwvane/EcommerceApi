using EcommerceApp.Models.DTO;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var data = _context.Options.Include(ot=> ot.OptionType).Select(o => new OptionDto
            {
                Id = o.OptionId,
                Name = $"{o.Name} ({o.Value} )",
                Value = o.Value,
                OptionType = o.OptionType
        });
            return new Result() { Data = data };
        }

        [HttpGet("GetOptionTypes")]
        public Result GetOptionTypes()
        {
            var data = _context.OptionsTypes.Select(o => new
            {
                Id = o.OptionTypeId,
                o.Name,
            });
            return new Result() { Data = data };
        }

        [HttpPost("AddOption")]
        public  async Task<IActionResult> AddOption([FromBody] OptionDto option)
        {
            var optionToSave = new Option()
            {
                Name = option.Name,
                OptionTypeId = option.OptionTypeId,
                Value = option.Value,
            };
            try
            {
                await _context.Options.AddAsync(optionToSave);
                await _context.SaveChangesAsync();
                return Ok(option);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOptionType")]
        public IActionResult AddOptionType([FromBody] OptionTypeDto optionType)
        {
            var optionTypeToSave = new OptionType()
            {
                Name = optionType.Name,
            };
            try
            {
                _context.OptionsTypes.Add(optionTypeToSave);
                _context.SaveChanges();
                return Ok(optionType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
