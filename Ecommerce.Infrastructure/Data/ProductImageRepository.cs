using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Data
{
    public  class ProductImageRepository: IProductImageRepository
    {
        private readonly Context _context;
        public ProductImageRepository(Context context)
        {
            _context = context;
        }

        public async Task<ProductImages?> AddAsync(ProductImages entity)
        {
            try
            {
                await _context.ProductImages.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("an error ocured during adding an product");
            };
        }

        public async Task<bool> AddListAsync(List<ProductImages> productImages)
        {
            try
            {
                await _context.ProductImages.AddRangeAsync(productImages);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("an error ocured during adding an product");
            };
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
