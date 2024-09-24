using Ecommerce.Core.Entities;
using EcommerceApp.Models.DTO;

namespace Ecommerce.Api.Extensions
{
    public static  class ManufacturerExtensions
    {
        public static ManufacturerDto? ToManufacturerDto(this Manufacturer manufacturer)
        {
            if (manufacturer == null) { return null; }
            return new ManufacturerDto()
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                Country =  new CountryDto()
                {
                    Id = manufacturer.Country.Id,
                    Name = manufacturer.Country.Name,
                    Image = manufacturer.Country.Image,
                }
            };
        }
        public static Manufacturer? ToManufacturer(this ManufacturerDto manufacturerDto)
        {
            if (manufacturerDto == null) { return null; }
            return new Manufacturer()
            {
                Id = manufacturerDto.Id,
                Name = manufacturerDto.Name,    
                CountryId = manufacturerDto.Country.Id,
            };
        }

        public static ICollection<ManufacturerDto> ToOptionDtoList(this ICollection<Manufacturer> manufacturers)
        {
            ICollection<ManufacturerDto> result = new List<ManufacturerDto>();
            foreach (var item in manufacturers)
            {
                var manufacturerDto = item.ToManufacturerDto();
                if (manufacturerDto != null)
                {
                    result.Add(manufacturerDto);
                }
            }
            return result;

        }
    }
}
