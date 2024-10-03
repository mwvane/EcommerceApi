using Ecommerce.Core.Entities;
using EcommerceApp.Models.DTO;
using Newtonsoft.Json.Linq;

namespace Ecommerce.Api.Extensions
{
    public static class CountryExtensions
    {

        public static CountryDto? ToCountryDto(this Country country)
        {
            if (country == null) { return null; }
            return new CountryDto()
            {
                Id = country.Id,
                Name = country.Name,
                //Image = country.Image,
            };
        }
        public static Country? ToCountry(this CountryDto country)
        {
            if (country == null) { return null; }
            return new Country()
            {
                Id = country.Id,
                Name = country.Name,
                //Image = country.Image,
            };
        }

        public static ICollection<CountryDto> ToCountryDtoList(this ICollection<Country> countries)
        {
            ICollection<CountryDto> result = new List<CountryDto>();
            foreach (var item in countries)
            {
                var countryDto = item.ToCountryDto();
                if (countryDto != null)
                {
                    result.Add(countryDto);
                }
            }
            return result;

        }

    }
}
