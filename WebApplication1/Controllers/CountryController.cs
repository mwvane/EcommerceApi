using EcommerceApp.Data;
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
        private readonly Context _context;
        public CountryController(Context context)
        {
            _context = context;
        }
        [HttpGet("GetCountries")]
        public Result GetCountries()
        {
            var data = _context.Countries.Select(c => new CountryDto()
            {
                Id = c.CountryId,
                Name = c.Name,
                Image = c.Image
            });
            return new Result() { Data = data };
        }

        [HttpDelete("DeleteCountry")]
        public async Task<Result> DeleteCountry([FromBody] List<int> countryIds)
        {
            foreach (var id in countryIds)
            {
                var country = await _context.Countries.FindAsync(id);
                if (country != null)
                {
                    _context.Countries.Remove(country);
                }
                else
                {
                    return new Result()
                    {
                        Notification = new Notification()
                        {
                            Message = $"failed:  country with id = {id} not found",
                            Status = NotificationStatus.Error,
                            Title = "couldn't deleted"
                        }
                    };
                };
            }
            try
            {
                await _context.SaveChangesAsync();
                return new Result()
                {
                    Notification = new Notification()
                    {
                        Message = "selected countries deleted successfully",
                        Status = NotificationStatus.Success,
                        Title = "successfully deleted"
                    }
                };

            }
            catch (Exception ex)
            {
                throw new NotFoundException("Failed to deletete selected countries");
            }
        }
    }
}
