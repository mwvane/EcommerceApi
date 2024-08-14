using Ecommerce.Core.Entities;
using EcommerceApp.Models.DTO;

namespace Ecommerce.Api.Extensions
{
    public static class OptionTypeExtensions
    {
        public static OptionTypeDto? ToOptionTypeDto(this OptionType optionType)
        {
            if (optionType == null) { return null; }
            return new OptionTypeDto()
            {
                Name = optionType.Name,
                Id = optionType.Id,
            };
        }

        public static OptionType? ToOptionType(this OptionTypeDto optionTypeDto)
        {
            if (optionTypeDto == null) { return null; }
            return new OptionType()
            {
                Name = optionTypeDto.Name,
                Id = optionTypeDto.Id
            };
        }
    }
}
