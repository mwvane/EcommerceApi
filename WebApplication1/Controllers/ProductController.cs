using AutoMapper;
using Ecommerce.Api.Extensions;
using Ecommerce.Api.Models;
using Ecommerce.Api.Notifications;
using Ecommerce.Application.Services;
using Ecommerce.Core.Entities;
using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcommerceApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly ProductImagesService _productImagesService;
        private readonly IMapper _mapper;
        public ProductController(ProductService productService, ProductImagesService productImagesService, IMapper mapper)
        {
            _productService = productService;
            _productImagesService = productImagesService;
            _mapper = mapper;
        }
        //[HttpGet("GetProducts")]
        //public Result GetProducts()
        //{
        //    var data = GetProductsByType();
        //    return new Result() { Data = data };
        //}

        [HttpPost("AddProduct"), DisableRequestSizeLimit]
        public async Task<Response> AddProduct([FromForm] IFormCollection data)
        {

            var images = data.Files;
            var productJson = data["product"];
            ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(productJson[0]);
            var product = _mapper.Map<Product>(productDto);
            var result = await _productService.AddAsync(product);
            if (result != null && result.Id != 0)
            {
                var stringImages = await FileService.SaveImageListToString(images, UploadType.ProductImages);

                var productImagesDtos = stringImages.ToOptionTypeDto(product.Id);

                var productImages = _mapper.Map<List<ProductImages>>(productImagesDtos);

                var imagesResult = await _productImagesService.AddListImmages(productImages);
                return new Response()
                {
                    Data = result,
                    Notification = DefaultNotifications.Success<Product>(CRUD_Action.Create)
                };
            }
            return new Response()
            {
                Notification = DefaultNotifications.Success<Product>(CRUD_Action.Create)
            };

        }

        [HttpGet("GetProducts"), DisableRequestSizeLimit]

        public async Task<Response> GetProducts()
        {
            var data = await _productService.GetAllAsync();
            if (data != null)
            {
                return new Response() { Data = data };
            }
            throw new Exception("Coulldnot load Products");
        }
        //[HttpDelete("DeleteProduct/{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if(product != null)
        //    {
        //        _context.Products.Remove(product);
        //        _context.SaveChanges();
        //         return Ok();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        //[HttpDelete("DeleteProducts")]
        //public async Task<IActionResult> DeleteProducts([FromBody] List<int> productIds)
        //{
        //    foreach (var id in productIds)
        //    {
        //        var product = await _context.Products.FindAsync(id);
        //        if (product != null)
        //        {
        //            _context.Products.Remove(product);
        //        }
        //    };
        //    _context.SaveChanges();
        //    return Ok();

        //}

        //private object GetProductsByType(ProductType productType = ProductType.None)
        //{
        //    var data = _context.Products
        //                .Include(p => p.ProductOptions)
        //                .ThenInclude(po => po.Option)
        //                .Include(p => p.Category)
        //                .Include(p => p.Manufacturer)
        //                .Include(p => p.Ratings)
        //                .Include(p => p.ProductImages)
        //                .Include(p => p.Discounts)
        //                .Include(p => p.ViewCounts)
        //                .Select(p => new
        //                {
        //                    ProductId = p.ProductId,
        //                    Name = p.Name,
        //                    Price = p.Price,
        //                    Description = p.Description,
        //                    Category = new CategoryDto { Id = p.Category.CategoryId, Name = p.Category.Name },
        //                    CreateDate = p.CreateDate,
        //                    Manufacturer = new ManufacturerDto { Id = p.ManufacturerId, Name = p.Manufacturer.Name },
        //                    Rating = p.Ratings.Any() ? p.Ratings.Average(r => r.Rating) : 0,
        //                    //ProductImages = p.ProductImages.Select(img => img.Url).ToList(),
        //                    ProductImages = p.ProductImages.Select(i => new ProductImagesDto { Url = Path.Combine(Helpers.hostName, i.Url) }).ToList(),
        //                    Discounts = p.Discounts.Select(d => new DiscountDto { Id = d.DiscountId, DiscountAmount = d.DiscountAmount, StartDate = d.StartDate, EndDate = d.EndDate }).ToList(),
        //                    ViewCount = p.ViewCounts.Count(),
        //                    Options = p.ProductOptions
        //                    .GroupBy(po => po.Option.OptionType)
        //                    .Select(g => g.Select(po => new OptionDto
        //                    {
        //                        OptionId = po.Option.OptionId,
        //                        Name = po.Option.Name,
        //                        Value = po.Option.Value,
        //                        OptionType = po.Option.OptionType,
        //                    }).ToList()).ToList()
        //                }).ToList();

        //    return data;
    }
}

public enum ProductType
{
    None = 0,
    Special = 1,
    Latest = 2,
    Trading = 3,
}

