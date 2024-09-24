using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using EcommerceApp.ErrorHandling;
using Ecommerce.Application.Services;
using Ecommerce.Api.Models;
using Ecommerce.Api.Notifications;
using Ecommerce.Core.Entities;
using Ecommerce.Api.Extensions;
using AutoMapper;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : Controller
    {
        private readonly OptionService _optionService;
        private readonly OptionTypeService _optionTypeService;
        private readonly IMapper _mapper;
        public OptionController(OptionService optionService, OptionTypeService optionTypeService, IMapper mapper)
        {
            _optionService = optionService;
            _optionTypeService = optionTypeService;
            _mapper = mapper;
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
            return new Response() { Data = _mapper.Map<List<OptionDto>>(data) };
        }

        [HttpGet("GetOptionTypes")]
        public async Task<Response> GetOptionTypes()
        {
            var data = await _optionTypeService.GetAllOptionTypes();
            return new Response() { Data = _mapper.Map<List<OptionTypeDto>>(data) };
        }

        [HttpPost("AddOption")]
        public async Task<Response> AddOption([FromBody] OptionDto option)
        {
            var notCreatedNotification = new Response() { Notification = DefaultNotifications.Error<Option>(CRUD_Action.Create) };

            var result = await _optionService.AddAsync(_mapper.Map<Option>(option));
            if (result != null)
            {
                return new Response()
                {
                    Data = _mapper.Map<OptionDto>(result),
                    Notification = DefaultNotifications.Success<Option>(CRUD_Action.Create)
                };
            }
            return notCreatedNotification;
        }

        [HttpPost("AddOptionType")]
        public async Task<Response> AddOptionType([FromBody] OptionTypeDto optionTypeDto)
        {
            var notCreatedNotification = new Response() { Notification = DefaultNotifications.Error<OptionType>(CRUD_Action.Create) };
            var result = await _optionTypeService.AddAsync(_mapper.Map<OptionType>(optionTypeDto));
            if (result != null)
            {
                return new Response()
                {
                    Data = optionTypeDto,
                    Notification = DefaultNotifications.Success<OptionType>(CRUD_Action.Create),
                };
            }
            return notCreatedNotification;
        }

        [HttpPut("UpdateOption")]
        public async Task<Response> UpdateOption([FromBody] OptionDto optionDto)
        {
            var result = await _optionService.UpdateAsync(_mapper.Map<Option>(optionDto));
            if (result)
            {
                return new Response()
                {
                    Notification = DefaultNotifications.Success<Option>(CRUD_Action.Update)
                };
            }
            return new Response()
            {
                Notification = DefaultNotifications.Error<Option>(CRUD_Action.Update)
            };
        }

        [HttpPut("UpdateOptionType")]
        public async Task<Response> UpdateOptionType([FromBody] OptionTypeDto optionTypeDto)
        {
            var optionType = optionTypeDto.ToOptionType();
            if (optionType != null)
            {
                var result = await _optionTypeService.UpdateAsync(optionType);
                if (result)
                {
                    return new Response()
                    {
                        Notification = DefaultNotifications.Success<OptionType>(CRUD_Action.Update)
                    };
                }

            }
            return new Response()
            {
                Notification = DefaultNotifications.Error<OptionType>(CRUD_Action.Update)
            };

        }

        [HttpDelete("DeleteOption")]
        public async Task<Response> DeleteOption([FromBody] List<int> optionIds)
        {

            bool isDeleted = await _optionService.DeleteAsync(optionIds);
            if (isDeleted)
            {
                return new Response()
                {
                    Notification = DefaultNotifications.Success<Option>(CRUD_Action.Delete)
                };
            }
            return new Response()
            {
                Notification = DefaultNotifications.Error<Option>(CRUD_Action.Delete)
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
                    Notification = DefaultNotifications.Success<OptionType>(CRUD_Action.Delete)
                };
            }
            return new Response()
            {
                Notification = DefaultNotifications.Error<OptionType>(CRUD_Action.Delete)
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
