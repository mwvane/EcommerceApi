using EcommerceApp.Models.DTO;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.ErrorHandling;

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
        [HttpPost("Test")]
        public IActionResult Test([FromBody] int id)
        {
            if (id <= 0)
            {
                throw new ItemNotFoundException($"item with ID {id} not found" );
            }

            return Ok(new { Id = id, Name = "Sample" });
        }

        [HttpGet("GetOptionById/{id}")]
        public async Task<IActionResult> GetOptionById(int id)
        {
            var option = await _context.Options.
                Select(o => new OptionDto
                {
                    OptionId = o.OptionId,
                    Name = o.Name,
                    OptionTypeId = o.OptionTypeId,
                    Value = o.Value

                }).SingleOrDefaultAsync(c => c.OptionId == id);
            if (option != null)
            {
                return Ok(option);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetOptionTypeById/{id}")]
        public async Task<Result> GetOptionTypeById(int id)
        {
            var optionType = await _context.OptionsTypes.
                Select(o => new OptionTypeDto
                {
                    Name = o.Name,
                    OptionTypeId = o.OptionTypeId,
                }).SingleOrDefaultAsync(o => o.OptionTypeId == id);
            if (optionType != null)
            {
                return new Result()
                {
                    Data = optionType,
                    Success = "true"
                };
            }
            else
            {
                 return new Result()
                {
                    Error = new List<string> { "option type not found" }
                };
            }
        }

        [HttpGet("GetOptions")]
        public Result GetOptions()
        {
            var data = _context.Options.Include(ot => ot.OptionType).Select(o => new OptionDto
            {
                OptionId = o.OptionId,
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
        public async Task<Result> AddOptionType([FromBody] OptionTypeDto optionType)
        {
            var newOptionType = new OptionType()
            {
                Name = optionType.Name,
            };
            try
            {
                await _context.OptionsTypes.AddAsync(newOptionType);
                await _context.SaveChangesAsync();
                return new Result()
                {
                    Data = newOptionType,
                    Success = "option type successfully updated"
                };
            }
            catch (Exception ex)
            {
                return new Result()
                {
                    Error = new List<string> { "faild to update option type,", ex.Message }
                };
            }
        }
        [HttpPut("UpdateOption")]
        public async Task<Result> UpdateOption([FromBody] OptionDto option)
        {
            var newOption = new Option()
            {

                OptionId = option.OptionId,
                Value = option.Value,
                Name = option.Name,
                OptionTypeId = option.OptionTypeId,
            };
            try
            {
                _context.Options.Update(newOption);
                await _context.SaveChangesAsync();
                return new Result()
                {
                    Data = newOption,
                    Success = "option successfully updated"
                };
            }
            catch (Exception ex)
            {
                return new Result()
                {
                    Error = new List<string> { "faild to update option,", ex.Message }
                };
            }

        }

        [HttpPut("UpdateOptionType")]
        public async Task<Result> UpdateOptionType([FromBody] OptionTypeDto option)
        {
            var newOptionType = new OptionType()
            {

                OptionTypeId = option.OptionTypeId,
                Name = option.Name,
            };
            try
            {
                _context.OptionsTypes.Update(newOptionType);
                await _context.SaveChangesAsync();
                return new Result()
                {
                    Data = newOptionType,
                    Success = "option type successfully updated"
                };
            }
            catch (Exception ex)
            {
                return new Result()
                {
                    Error = new List<string> { "faild to update option type,", ex.Message }
                };
            }

        }

        [HttpDelete("DeleteOption")]
        public async Task<Result> DeleteOption([FromBody] List<int> optionIds)
        {
            foreach (var id in optionIds)
            {
                var option = await _context.Options.FindAsync(id);
                if (option != null)
                {
                    _context.Options.Remove(option);
                }
                else
                {
                    return new Result() { Error = new List<string>() { $"option with id = {id} not found" } };
                }
            };
            _context.SaveChanges();
            return new Result() { Success = "options successfully deleted" };

        }

        [HttpDelete("DeleteOptionType")]
        public async Task<Result> DeleteOptionType([FromBody] List<int> optionTpeIds)
        {
            foreach (var id in optionTpeIds)
            {
                var optionType = await _context.OptionsTypes.FindAsync(id);
                if (optionType != null)
                {
                    _context.OptionsTypes.Remove(optionType);
                }
                else
                {
                    return new Result() { Error = new List<string>() { $"option type with id = {id} not found" } };
                }
            };
            _context.SaveChanges();
            return new Result() { Success = "option type successfully deleted" };

        }
    }
}
