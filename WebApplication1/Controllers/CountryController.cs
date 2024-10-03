using AutoMapper;
using Ecommerce.Api.Extensions;
using Ecommerce.Api.Models;
using Ecommerce.Api.Notifications;
using Ecommerce.Application.DTOs;
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
        private readonly IMapper _mapper;
        public CountryController(CountryService countryService, IMapper mapper)
        {
           _countryService = countryService;
            _mapper = mapper;
        }

        [HttpPost("CreateCountry")]
        public async Task<Response> CreateCountry([FromForm] CreateCountryDto country)
        {
            if(country.Image.Length > 0)
            {
                var imagePath = await FileService.SaveImageToStorage(country.Image, UploadType.Countries);
                var result = await _countryService.AddAsync(new Country() { Name = country.Name, Image = imagePath });
                if(result.Id != null || result.Id != 0)
                {
                    return new Response() { Data = _mapper.Map<CountryDto>(result), Notification = DefaultNotifications.Success<Country>(CRUD_Action.Create) };
                }
                return new Response() { Notification = DefaultNotifications.Error<Country>(CRUD_Action.Create) };
            }
            else
            {
                return new Response() { Notification = DefaultNotifications.Error<Country>(CRUD_Action.Create, "You mast upload a photo") };
            }

            //if (image != null && image.Length > 0)
            //{
            //    var imagePath = Path.Combine("wwwroot/images", image.FileName);
            //    using (var stream = new FileStream(imagePath, FileMode.Create))
            //    {
            //        await image.CopyToAsync(stream);
            //    }
            //    country.Image = imagePath;
            //}

            // Save country details to the database
            // _context.Countries.Add(country);
            // await _context.SaveChangesAsync();

        }

        [HttpGet("GetCountries")]
        public async Task<Response> GetCountries()
        {
            var countries = await _countryService.GetAllAsync();
            if (countries != null)
            {
                return new Response() { Data = _mapper.Map<List<CountryDto>>(countries) };
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

        [HttpDelete("DeleteCountry")]
        public async Task<Response> DeleteCountry([FromBody] List<int> countryIds)
        {
            var result = await _countryService.DeleteAsync(countryIds);
            if (result)
            {
                return new Response() { Notification = DefaultNotifications.Success<Country>(CRUD_Action.Delete) };
            }
            return new Response() { Notification = DefaultNotifications.Error<Country>(CRUD_Action.Delete)};
        }
    }
}
