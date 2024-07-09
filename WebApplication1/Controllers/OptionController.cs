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
        [HttpGet("GetOptionById/{id}")]
        public async Task<IActionResult> GetOptionById(int id)
        {
            var option = await _context.Options.
                Select(o => new OptionDto
                {
                    Id = o.OptionId,
                    Name = o.Name,
                    OptionTypeId = o.OptionTypeId,
                    Value = o.Value

                }).SingleOrDefaultAsync(c => c.Id == id);
            if (option != null)
            {
                return Ok(option);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetOptions")]
        public Result GetOptions()
        {
            var data = _context.Options.Include(ot => ot.OptionType).Select(o => new OptionDto
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
        public async Task<IActionResult> AddOption([FromBody] OptionDto option)
        {
            var optionToSave = new Option()
            {
                OptionId = option.Id,
                Name = option.Name,
                OptionTypeId = option.OptionTypeId,
                Value = option.Value,
            };
            try
            {
                await _context.Options.AddAsync(optionToSave);
                await _context.SaveChangesAsync();
                return Ok(optionToSave);
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
        [HttpPut("UpdateOption")]
        public async Task<IActionResult> UpdateOption([FromBody] OptionDto option)
        {
            var newOption = new Option()
            {

                OptionId = option.Id,
                Value = option.Value,
                Name = option.Name,
                OptionTypeId = option.OptionTypeId,
            };
            try
            {
                _context.Options.Update(newOption);
                await _context.SaveChangesAsync();
                return Ok(newOption);
            }
            catch (Exception ex)
            {
                return BadRequest("Manufacturer not updated," + ex.Message);
            }

        }
    }
}
