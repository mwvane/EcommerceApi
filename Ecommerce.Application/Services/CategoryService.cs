using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class CategoryService : ICrudService<Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        { 
            _categoryRepository = categoryRepository;
        }
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
            var catgories = await _categoryRepository.GetAllAsync();
            return catgories.Reverse().ToList();
        }

        public Task<Category?> GetByIdAsync(int id)
        {
            return _categoryRepository.GetByIdAsync(id);
        }

        public Task<bool> UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
