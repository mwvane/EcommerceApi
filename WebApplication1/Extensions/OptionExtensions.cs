using EcommerceApp.Models;
using EcommerceApp.Models.DTO;

namespace EcommerceApp.Extensions
{
    public static  class OptionExtensions
    {
        public static OptionDto? ToOptionDoto(this Option option)
        {
            if(option == null) { return null; }
            return new OptionDto()
            {
                Name = option.Name,
                Value = option.Value,
                OptionId = option.OptionId, 
                OptionType = option.OptionType,
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
                OptionId = option.OptionId,
                OptionType = option.OptionType,
                OptionTypeId = option.OptionTypeId,
            };
        }
    }
}
