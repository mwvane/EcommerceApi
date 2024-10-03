using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class ProductImagesService : ICrudService<ProductImages>
    {
        private readonly IProductImageRepository _productImageRepository;
        public ProductImagesService(IProductImageRepository productImageRepository) { _productImageRepository = productImageRepository; }
        public Task<ProductImages?> AddAsync(ProductImages entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> AddListImmages(List<ProductImages> productImages)
        {
            return _productImageRepository.AddListAsync(productImages);
        }

        public Task<bool> DeleteAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ProductImages>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductImages?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ProductImages entity)
        {
            throw new NotImplementedException();
        }
    }
}
