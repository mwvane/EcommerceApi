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
    public class CountryController : Controller
    {
        private readonly CountryService _countryService;
        public CountryController(CountryService countryService)
        {
           _countryService = countryService;
        }
        [HttpGet("GetCountries")]
        public async Task<Response> GetCountries()
        {
            var countries = await _countryService.GetAllAsync();
            if (countries != null)
            {
                var countryDtos = countries.ToCountryDtoList();
                return new Response() { Data = countryDtos };
            }
            return new Response() { };
        }

        [HttpGet("GetCountryById/{id}")]
        public async Task<Response> GetCountryById(int id)
        {
            var country = await _countryService.GetByIdAsync(id);
            if (country != null)
            {
                var countryDto = country.ToCountryDto();
                return new Response() { Data = countryDto};
            }
            return new Response() { };
        }

        [HttpGet("UpdateCountry")]
        public async Task<Response> UpdateCountry([FromBody] Country country)
        {
            var result = await _countryService.UpdateAsync(country);
            if (result)
            {
                return new Response()
                {
                    Notification = DefaultNotifications.Success<Country>(CRUD_Action.Update)
                };
            }
            return new Response() { };
        }

        //[HttpDelete("DeleteCountry")]
        //public async Task<Result> DeleteCountry([FromBody] List<int> countryIds)
        //{
        //    foreach (var id in countryIds)
        //    {
        //        var country = await _context.Countries.FindAsync(id);
        //        if (country != null)
        //        {
        //            _context.Countries.Remove(country);
        //        }
        //        else
        //        {
        //            return new Result()
        //            {
        //                Notification = new Notification()
        //                {
        //                    Message = $"failed:  country with id = {id} not found",
        //                    Status = NotificationStatus.Error,
        //                    Title = "couldn't deleted"
        //                }
        //            };
        //        };
        //    }
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return new Result()
        //        {
        //            Notification = new Notification()
        //            {
        //                Message = "selected countries deleted successfully",
        //                Status = NotificationStatus.Success,
        //                Title = "successfully deleted"
        //            }
        //        };

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new NotFoundException("Failed to deletete selected countries");
        //    }
        //}
    }
}
