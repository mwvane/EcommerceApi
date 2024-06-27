using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace EcommerceApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly Context _context;
        public ProductController(Context context)
        {
            _context = context;
        }
        [HttpPost("CreateProduct")]
        public Result CreateProduct([FromBody] Product product)
        {
            //_context.Products.Add(new Product()
            //{
            //    Name = "ბოტასი",
            //    Price = 100,
            //    Quantity = 150,
            //    CategoryId = 1,
            //    Description = "სატესტო პროდუქტი"
            //});
            //_context.SaveChanges();
            return new Result() { Data = null };
        }
        [HttpGet("GetProducts")]
        public Result GetProducts()
        {
            var data = GetProductsByType();
            return new Result() { Data = data };
        }

        [HttpPost("AddProduct"),DisableRequestSizeLimit]
        public Result AddProduct([FromForm] IFormCollection data)
        {
            var images = data.Files;
            var productJson = data["product"];
            ProductDto product = JsonConvert.DeserializeObject<ProductDto>(productJson[0]);
            return new Result() { Data = product };
        }

        private object GetProductsByType(ProductType productType = ProductType.None)
        {
            var data = _context.Products
                        .Include(p => p.ProductOptions)
                        .ThenInclude(po => po.Option)
                        .Include(p => p.Category)
                        .Include(p => p.Manufacturer)
                        .Include(p => p.Ratings)
                        .Include(p => p.ProductImages)
                        .Include(p => p.Discounts)
                        .Include(p => p.ViewCounts)
                        .Select(p => new 
                        {
                            ProductId = p.ProductId,
                            Name = p.Name,
                            Price = p.Price,
                            Description = p.Description,
                            Category  = new CategoryDto{ Id = p.Category.CategoryId, Name = p.Category.Name },
                            CreateDate = p.CreateDate,
                            Manufacturer = new ManufacturerDto { Id = p.ManufacturerId,  Name = p.Manufacturer.Name},
                            Rating = p.Ratings.Any() ? p.Ratings.Average(r => r.Rating) : 0,
                            //ProductImages = p.ProductImages.Select(img => img.Url).ToList(),
                            ProductImages = p.ProductImages.Select(i => new ProductImagesDto { Url = i.Url }).ToList(),
                            Discounts = p.Discounts.Select(d => new DiscountDto {Id =  d.DiscountId, DiscountAmount =  d.DiscountAmount, StartDate =  d.StartDate, EndDate =  d.EndDate }).ToList(),
                            ViewCount = p.ViewCounts.Count(),
                            Options = p.ProductOptions
                            .GroupBy(po => po.Option.OptionType)
                            .Select(g => g.Select(po => new OptionDto
                            {
                                Id = po.Option.OptionId,
                                Name = po.Option.Name,
                                Value = po.Option.Value,
                                OptionType = po.Option.OptionType,
                            }).ToList()).ToList()
                        }).ToList();

            return data;
        }
    }

    public enum ProductType
    {
        None = 0,
        Special = 1,
        Latest = 2,
        Trading = 3,
    }
}
