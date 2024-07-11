using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
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
        [HttpGet("GetProducts")]
        public Result GetProducts()
        {
            var data = GetProductsByType();
            return new Result() { Data = data };
        }

        [HttpPost("AddProduct"), DisableRequestSizeLimit]
        public async Task<Result> AddProduct([FromForm] IFormCollection data)
        {
            var images = data.Files;
            var productJson = data["product"];
            ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(productJson[0]);
            var product = new Product
            {
                ManufacturerId = productDto!.Manufacturer.Id,
                CategoryId = productDto.Category.Id,
                Description = productDto.Description!,
                Name = productDto.Name,
                Price = productDto.Price,
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            //set options
            foreach (var item in productDto.Options)
            {
                _context.ProductOptions.Add(new ProductOption { OptionId = item.OptionId, ProductId = product.ProductId });
            }
            await _context.SaveChangesAsync();

            // SAVE IMAGES
            var urls = await FileService.SaveFile(new UploadFile { Id = product.ProductId, File = images, UploadType = UploadType.ProductImage });

            //Bind images to product
            if (urls != null)
            {
                foreach (var url in urls)
                {
                    _context.ProductImages.Add(new ProductImages { ProductId = product.ProductId, Url = url });
                }
                _context.SaveChanges();
            }


            return new Result() { Data = product };
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                 return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("DeleteProducts")]
        public async Task<IActionResult> DeleteProducts([FromBody] List<int> productIds)
        {
            foreach (var id in productIds)
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                }
            };
            _context.SaveChanges();
            return Ok();

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
                            Category = new CategoryDto { Id = p.Category.CategoryId, Name = p.Category.Name },
                            CreateDate = p.CreateDate,
                            Manufacturer = new ManufacturerDto { Id = p.ManufacturerId, Name = p.Manufacturer.Name },
                            Rating = p.Ratings.Any() ? p.Ratings.Average(r => r.Rating) : 0,
                            //ProductImages = p.ProductImages.Select(img => img.Url).ToList(),
                            ProductImages = p.ProductImages.Select(i => new ProductImagesDto { Url = Path.Combine(Helpers.hostName, i.Url) }).ToList(),
                            Discounts = p.Discounts.Select(d => new DiscountDto { Id = d.DiscountId, DiscountAmount = d.DiscountAmount, StartDate = d.StartDate, EndDate = d.EndDate }).ToList(),
                            ViewCount = p.ViewCounts.Count(),
                            Options = p.ProductOptions
                            .GroupBy(po => po.Option.OptionType)
                            .Select(g => g.Select(po => new OptionDto
                            {
                                OptionId = po.Option.OptionId,
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
