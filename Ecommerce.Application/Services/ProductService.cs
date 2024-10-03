using Ecommerce.Application.Helpers;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class ProductService : ICrudService<Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) { _productRepository = productRepository; }
        public Task<Product?> AddAsync(Product product)
        {
            product.Category = null;
            product.Manufacturer = null;
            return _productRepository.AddAsync(product);
        }

        public Task<bool> DeleteAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            var data = await _productRepository.GetAllAsync();
            foreach (var item in data)
            {
                foreach (var image in item.ProductImages)
                {
                    if(image != null && HostHeplers.ipAddress != null)
                    {
                        image.Url = HostHeplers.url +'/'+ image.Url;
                    }
                }
                
            }
            return data ;
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
