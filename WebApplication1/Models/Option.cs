namespace EcommerceApp.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int OptionTypeId { get; set; }
        public OptionType OptionType { get; set; }
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
