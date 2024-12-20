﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.Core.Entities
{
    public class Option
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public int OptionTypeId { get; set; }
        public OptionType OptionType { get; set; }
        [JsonIgnore]
        public ICollection<ProductOption> ProductOptions { get; set; }

    }
    //public enum OptionType
    //{
    //    None = 0,
    //    Color = 1,
    //    ScreenType = 2,
    //    ScreenSize = 3,
    //    ScreenDimesion = 4,
    //    ScreenLight = 5,
    //    Memory = 6,
    //    RAM = 7,
    //    CPU = 8,
    //    GPU = 9,
    //}
}
