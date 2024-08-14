using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using EcommerceApp.ErrorHandling;
using Ecommerce.Application.Services;
using Ecommerce.Api.Models;
using EcommerceApp.Extensions;
using Ecommerce.Api.Notifications;
using Ecommerce.Core.Entities;
using Ecommerce.Api.Extensions;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : Controller
    {
        private readonly OptionService _optionService;
        private readonly OptionTypeService _optionTypeService;
        public OptionController(OptionService optionService, OptionTypeService optionTypeService)
        {
            _optionService = optionService;
            _optionTypeService = optionTypeService;
        }

        [HttpGet("GetOptionById/{id}")]
        public async Task<Response> GetOptionById(int id)
        {
            var result = await _optionService.GetByIdAsync(id);
            if (result == null)
            {
                throw new ItemNotFoundException($"option with ID - {id} not found");
            }
            return new Response() { Data = result };
        }

        [HttpGet("GetOptionTypeById/{id}")]
        public async Task<Response> GetOptionTypeById(int id)
        {
            var result = await _optionTypeService.GetByIdAsync(id);
            return new Response()
            {
                Data = result
            };
        }

        [HttpGet("GetOptions")]
        public async Task<Response> GetOptions()
        {
            var data = await _optionService.GetAllAsync();
            return new Response() { Data = data.ToOptionDto() };
        }

        [HttpGet("GetOptionTypes")]
        public async Task<Response> GetOptionTypes()
        {
            var data = await _optionTypeService.GetAllOptionTypes();
            return new Response() { Data = data };
        }

        [HttpPost("AddOption")]
        public async Task<Response> AddOption([FromBody] OptionDto option)
        {
            var convertedOption = option.ToOption();
            var notCreatedNotification = new Response();

            if (convertedOption != null)
            {
                var result = await _optionService.AddAsync(convertedOption);
                if(result != null)
                {
                    return new Response()
                    {
                        Data = result,
                        Notification = DefaultNotifications.SuccessfullyCreate<Option>()
                    };
                }
                else
                {
                    return notCreatedNotification;
                }
            }
            return notCreatedNotification;
        }

        [HttpPost("AddOptionType")]
        public async Task<Response> AddOptionType([FromBody] OptionTypeDto optionTypeDto)
        {
            var optionType = optionTypeDto.ToOptionType();
            if(optionType != null)
            {
                var result = await _optionTypeService.AddAsync(optionType);
                if(result != null)
                {
                    return new Response()
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
                return new Response()
                {
                    Notification = new Notification()
                    {
                        Message = $"option type could't created",
                        Status = NotificationStatus.Error,
                        Title = "Error"
                    }
                };
            }
            else
            {
                throw new Exception("Option type could not created");
            }
        }

        [HttpPut("UpdateOption")]
        public async Task<Response> UpdateOption([FromBody] OptionDto optionDto)
        {
            var option = optionDto.ToOption();
            if (option != null)
            {
                var result = await _optionService.UpdateAsync(option);
                if (result)
                {
                    return new Response()
                    {
                        Notification = new Notification()
                        {
                            Title = NotificationStatus.Success.ToString(),
                            Message = "Uodated",
                            Status = NotificationStatus.Success
                        }
                    };
                }
            }
            return new Response()
            {
                Notification = new Notification()
                {
                    Message = $"option not updated",
                    Status = NotificationStatus.Error,
                    Title = "error"
                }
            };
        }

        [HttpPut("UpdateOptionType")]
        public async Task<Response> UpdateOptionType([FromBody] OptionTypeDto optionTypeDto)
        {
            var optionType = optionTypeDto.ToOptionType();
            if(optionType != null)
            {
                var result = await _optionTypeService.UpdateAsync(optionType);
                if (result)
                {
                    return new Response()
                    {
                        Notification = new Notification()
                        {
                            Title = NotificationStatus.Success.ToString(),
                            Message = "option type updated successfuly",
                            Status = NotificationStatus.Success
                        }
                    };
                }
                
            }
            return new Response()
            {
                Notification = new Notification()
                {
                    Title = NotificationStatus.Error.ToString(),
                    Message = "option type couldn't updated",
                    Status = NotificationStatus.Error
                }
            };

        }

        [HttpDelete("DeleteOption")]
        public async Task<Response> DeleteOption([FromBody] List<int> optionIds)
        {

           bool isDeleted =  await _optionService.DeleteAsync(optionIds);
            if (isDeleted)
            {
                return new Response()
                {
                    Notification = new Notification()
                    {
                        Message = $"slected options successfully deleted",
                        Status = NotificationStatus.Success,
                        Title = "succesfully deleted"
                    }
                };
            }
            return new Response()
            {
                Notification = new Notification()
                {
                    Message = $"slected options couldn't deleted",
                    Status = NotificationStatus.Error,
                    Title = "Error"
                }
            };

        }

        [HttpDelete("DeleteOptionType")]
        public async Task<Response> DeleteOptionType([FromBody] List<int> optionTpeIds)
        {
            var result = await _optionTypeService.DeleteAsync(optionTpeIds);
            if (result)
            {
                return new Response()
                {
                    Notification = new Notification()
                    {
                        Message = $"slected option types successfully deleted",
                        Status = NotificationStatus.Success,
                        Title = "succesfully deleted"
                    }
                };
            }
            return new Response()
            {
                Notification = new Notification()
                {
                    Message = $"somting went wrong during delete selected option types",
                    Status = NotificationStatus.Error,
                    Title = "error"
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
