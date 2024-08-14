using Ecommerce.Core.Entities;
using EcommerceApp.Models;
using EcommerceApp.Models.DTO;

namespace EcommerceApp.Extensions
{
    public static  class OptionExtensions
    {
        public static OptionDto? ToOptionDto(this Option option)
        {
            if(option == null) { return null; }
            return new OptionDto()
            {
                Name = option.Name,
                Value = option.Value,
                Id = option.Id, 
                OptionType = new OptionTypeDto()
                {
                    Id = option.OptionTypeId,
                    Name = option.Name,
                },
                OptionTypeId = option.OptionTypeId
            };
        }
        public static Option? ToOption(this OptionDto option)
        {
            if (option == null) { return null; }
            return new Option()
            {
                Name = option.Name,
                Value = option.Value,
                Id = option.Id,
                OptionTypeId = option.OptionTypeId,
            };
        }

        public static ICollection<OptionDto> ToOptionDto(this ICollection<Option> options)
        {
            ICollection<OptionDto> result = new List<OptionDto>();
            foreach (var item in options)
            {
                var optionDto = item.ToOptionDto();
                if (optionDto != null)
                {
                    result.Add(optionDto);
                }
            }
            return result;

        }
    }
}
