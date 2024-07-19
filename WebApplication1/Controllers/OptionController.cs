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
                throw new ItemNotFoundException($"option with ID {id} not found");
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
                throw new ItemNotFoundException($"option type with ID {id} not found");
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
        public async Task<Result> AddOption([FromBody] OptionDto option, CRUD_Action action = CRUD_Action.Create)
        {
            if (_context.Options.Any(o => o.Name.ToLower() == option.Name.ToLower()))
            {
                return new Result()
                {
                    Notification = new Notification()
                    {
                        Message = "option already exists",
                        Status = NotificationStatus.Warning,
                        Title = "already exists"
                    }

                };
            }
            var optionToSave = new Option()
            {
                OptionId = option.OptionId,
                Name = option.Name,
                OptionTypeId = option.OptionTypeId,
                Value = option.Value,
            };
            try
            {
                if (action == CRUD_Action.Create)
                {
                    await _context.Options.AddAsync(optionToSave);
                    await _context.SaveChangesAsync();
                }
                else if (action == CRUD_Action.Update)
                {
                    _context.Options.Update(optionToSave);
                    await _context.SaveChangesAsync();
                }

                return new Result()
                {
                    Data = optionToSave,
                    Notification = new Notification()
                    {
                        Message = $"option successfully {action.ToString()}d ",
                        Status = NotificationStatus.Success,
                        Title = $"successfully {action.ToString()}d"
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"faild to {action.ToString()} option" + ex.Message);
            }
        }

        [HttpPost("AddOptionType")]
        public async Task<Result> AddOptionType([FromBody] OptionTypeDto optionType, CRUD_Action action = CRUD_Action.Create)
        {
            if (_context.OptionsTypes.Any(o => o.Name.ToLower() == optionType.Name.ToLower()))
            {
                return new Result()
                {
                    Notification = new Notification()
                    {
                        Message = "option type already exists",
                        Status = NotificationStatus.Warning,
                        Title = "already exists"
                    }

                };
            }
            var newOptionType = new OptionType()
            {
                OptionTypeId = optionType.OptionTypeId,
                Name = optionType.Name,
            };
            try
            {
                if (action == CRUD_Action.Create)
                {
                    await _context.OptionsTypes.AddAsync(newOptionType);
                }
                else if (action == CRUD_Action.Update)
                {
                    _context.OptionsTypes.Update(newOptionType);
                }
                await _context.SaveChangesAsync();

                return new Result()
                {
                    Data = newOptionType,
                    Notification = new Notification()
                    {
                        Message = $"option type successfully {action.ToString()}d ",
                        Status = NotificationStatus.Success,
                        Title = $"successfully {action.ToString()}d"
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"faild to {optionType.ToString} option type : " + ex.Message);
            }
        }
        [HttpPut("UpdateOption")]
        public async Task<Result> UpdateOption([FromBody] OptionDto option)
        {
            var result = await AddOption(option, CRUD_Action.Update);
            return result;
        }

        [HttpPut("UpdateOptionType")]
        public async Task<Result> UpdateOptionType([FromBody] OptionTypeDto optionType)
        {
            var result = await AddOptionType(optionType, CRUD_Action.Update);
            return result;

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
                    return new Result()
                    {
                        Notification = new Notification()
                        {
                            Message = $"slected options Couldn't deleted . option with id = {id} not found",
                            Status = NotificationStatus.Error,
                            Title = "Couldn't delete"
                        },
                    };
                }
            };
            await _context.SaveChangesAsync();
            return new Result()
            {
                Notification = new Notification()
                {
                    Message = $"slected oprions successfully deleted",
                    Status = NotificationStatus.Success,
                    Title = "succesfully deleted"
                }
            };

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
                    return new Result()
                    {
                        Notification = new Notification()
                        {
                            Message = $"slected option types couldn't deleted . option type with id = {id} not found",
                            Status = NotificationStatus.Error,
                            Title = "Couldn't delete"
                        },
                    };
                }
            };
            await _context.SaveChangesAsync();
            return new Result()
            {
                Notification = new Notification()
                {
                    Message = $"slected option types successfully deleted",
                    Status = NotificationStatus.Success,
                    Title = "succesfully deleted"
                }
            };

        }
    }
    public enum CRUD_Action
    {
        Create,
        Update,
        Delete,
    }

}
