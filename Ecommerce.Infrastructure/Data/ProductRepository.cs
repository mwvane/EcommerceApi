using Ecommerce.Application.Helpers;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;
        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<Product?> AddAsync(Product entity)
        {
            try
            {
                await _context.Products.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
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

        public async Task<ICollection<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.ProductOptions)
                .ThenInclude(po => po.Option)
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Ratings)
                .Include(pi => pi.ProductImages)
                .Include(p => p.Discounts)
                .Include(p => p.ViewCounts)
                .ToListAsync();
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ProductNameExistsAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
