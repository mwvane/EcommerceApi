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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context) { _context = context; }
        public Task<Category?> AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.SubCategories).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var data = await _context.Categories.Include(o => o.SubCategories).FirstOrDefaultAsync(o => o.Id == id);
            return data;
        }

        public Task<bool> IsCategoryNameExist(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
