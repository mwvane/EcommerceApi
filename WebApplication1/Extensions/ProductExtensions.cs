using Ecommerce.Core.Entities;
using EcommerceApp.Models.DTO;

namespace Ecommerce.Api.Extensions
{
    public static class ProductExtensions
    {
        public static List<ProductImagesDto>? ToOptionTypeDto(this List<string> imgeUrls, int id)
        {
            var res = new List<ProductImagesDto>();
            foreach (var item in imgeUrls)
            {
                res.Add(new ProductImagesDto() { ProductId = id, Url = item });
            }
            return res;
        }
    }
}
