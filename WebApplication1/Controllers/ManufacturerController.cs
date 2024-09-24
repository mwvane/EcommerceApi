using Ecommerce.Api.Extensions;
using Ecommerce.Api.Models;
using Ecommerce.Api.Notifications;
using Ecommerce.Application.Services;
using Ecommerce.Core.Entities;
using EcommerceApp.ErrorHandling;
using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : Controller
    {
        private readonly ManufacturerService _manufacturerService;
        public ManufacturerController(ManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }
        [HttpGet("GetManufacturerById/{id}")]
        public async Task<Response> GetManufacturerById(int id)
        {
            var manufacturer =await _manufacturerService.GetByIdAsync(id);
            if (manufacturer != null) {
                return new Response()
                {
                    Data = manufacturer
                };
            }
            throw new NotFoundException($"Manufacturer with ID '{id}' not found");
        }

        [HttpGet("GetManufacturers")]
        public async Task<Response> GetManufacturers()
        {
            var manufacturers = await _manufacturerService.GetAllAsync();
            return new Response()
            {
                Data = manufacturers.ToOptionDtoList()
            };
        }

        [HttpPost("AddManufacturer")]
        public async Task<Response> AddManufacturer([FromBody] ManufacturerDto manufacturerDto, CRUD_Action action = CRUD_Action.Create)
        {
            var manufacturer = manufacturerDto.ToManufacturer();
            if (manufacturer != null)
            {
                var result = await _manufacturerService.AddAsync(manufacturer);
                return new Response()
                {
                    Data = result.ToManufacturerDto(),
                    Notification = DefaultNotifications.Success<Manufacturer>(CRUD_Action.Create)
                };
            }


            return new Response()
            {
                Notification = DefaultNotifications.Error<Manufacturer>(CRUD_Action.Create)
            };
        }



        [HttpPut("UpdateManufacturer")]
        public async Task<Response> UpdateManufacturer([FromBody] ManufacturerDto manufacturerDto)
        {
            var manufacturer = manufacturerDto.ToManufacturer();
            var error = new Response()
            {
                Notification = DefaultNotifications.Success<Manufacturer>(CRUD_Action.Update)
            };

            if (manufacturer != null)
            {
                var result = await _manufacturerService.UpdateAsync(manufacturer);
                if (result)
                {
                    return new Response()
                    {
                        Data = manufacturerDto,
                        Notification = DefaultNotifications.Success<Manufacturer>(CRUD_Action.Update)
                    };
                }
                return error;
            }
            return error;

        }

        [HttpDelete("DeleteManufacturer")]
        public async Task<Response> DeleteManufacturer([FromBody] List<int> manufacturerIds)
        {
            var result = await _manufacturerService.DeleteAsync(manufacturerIds);
            if (result)
            {
                return new Response()
                {
                    Notification = DefaultNotifications.Success<Manufacturer>(CRUD_Action.Delete)
                };
            }
            return new Response()
            {
                Notification = DefaultNotifications.Error<Manufacturer>(CRUD_Action.Delete)
            };

        }
    }
}

