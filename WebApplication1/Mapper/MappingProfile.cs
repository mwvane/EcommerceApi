using AutoMapper;
using Ecommerce.Core.Entities;
using EcommerceApp.Models.DTO;

namespace Ecommerce.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Option, OptionDto>();
            CreateMap<OptionDto, Option>();
            CreateMap<OptionType, OptionTypeDto>();
            CreateMap<OptionTypeDto, OptionType>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<ManufacturerDto, Manufacturer>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            
        }
    }
}
