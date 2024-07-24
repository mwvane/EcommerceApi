using EcommerceApp.Models.DTO;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.ErrorHandling;
using EcommerceApp.Data;
using EcommerceApp.Services;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : Controller
    {
        private readonly Context _context;
        private readonly IOptionService _optionService;
        private readonly IOptionTypeService _optionTypeService;
        public OptionController(Context context, IOptionService optionService, IOptionTypeService optionTypeService)
        {
            _context = context;
            _optionService = optionService;
            _optionTypeService = optionTypeService;
        }

        [HttpGet("GetOptionById/{id}")]
        public async Task<Result> GetOptionById(int id)
        {
            var result = await _optionService.GetItemByIdAsync(id);
            return new Result() { Data = result };
        }

        [HttpGet("GetOptionTypeById/{id}")]
        public async Task<Result> GetOptionTypeById(int id)
        {
            var result = await _optionTypeService.GetItemByIdAsync(id);
            return new Result()
            {
                Data = result
            };
        }

        [HttpGet("GetOptions")]
        public async Task<Result> GetOptions()
        {
            var data = await _optionService.GetAllItemsAsync();
            return new Result() { Data = data };
        }

        [HttpGet("GetOptionTypes")]
        public async Task<Result> GetOptionTypes()
        {
            var data = await _optionTypeService.GetAllItemsAsync();
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
            var result = await _optionService.AddItemAsync(option);
            if (result != null)
            {
                return new Result()
                {
                    Data = result,
                    Notification = new Notification()
                    {
                        Message = $"option with name '{option.Name.ToUpper()}' successfully created ",
                        Status = NotificationStatus.Success,
                        Title = $"successfully created"
                    }
                };
            }
            else
            {
                return new Result()
                {
                    Data = result,
                    Notification = new Notification()
                    {
                        Message = $"option with name '{option.Name.ToUpper()}' not saved",
                        Status = NotificationStatus.Error,
                        Title = $"not saved"
                    }
                };
            }
        }

        [HttpPost("AddOptionType")]
        public async Task<Result> AddOptionType([FromBody] OptionTypeDto optionType, CRUD_Action action = CRUD_Action.Create)
        {

            var result = await _optionTypeService.AddItemAsync(optionType);
            return new Result()
            {
                Data = result,
                Notification = new Notification()
                {
                    Message = $"option type successfully created",
                    Status = NotificationStatus.Success,
                    Title = "successfully created"
                }
            };
        }
        [HttpPut("UpdateOption")]
        public async Task<Result> UpdateOption([FromBody] OptionDto option)
        {
            await _optionService.UpdateItemAsync(option);
            return new Result()
            {
                Notification = new Notification()
                {
                    Message = $"option successfully updated",
                    Status = NotificationStatus.Success,
                    Title = "successfuly updated"
                }
            };
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

            await _optionService.DeleteItemAsync(optionIds);
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
