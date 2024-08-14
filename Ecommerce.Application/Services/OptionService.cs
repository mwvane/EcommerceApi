using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using EcommerceApp.Models;
using Microsoft.VisualBasic.FileIO;

namespace Ecommerce.Application.Services
{
    public class OptionService : ICrudService<Option>
    {
        private readonly IOptionRepository _optionRepository;
        public OptionService(IOptionRepository optionRepository) { _optionRepository = optionRepository; }

        public async Task<ICollection<Option>> GetAllAsync()
        {
            return await _optionRepository.GetAllAsync();
        }

        public async Task<Option?> GetByIdAsync(int id)
        {
            return await _optionRepository.GetByIdAsync(id);
        }

        public async Task<Option?> AddAsync(Option entity)
        {
            if (!await _optionRepository.OptionNameExistsAsync(entity.Name))
            {
                return await _optionRepository.AddAsync(entity);
            }
            else
            {
                throw new Exception($"option with name '{entity.Name}' already exists ");
            }
        }

        public async Task<bool> UpdateAsync(Option entity)
        {
            if (!await _optionRepository.OptionNameExistsAsync(entity.Name))
            {
                return await _optionRepository.UpdateAsync(entity);
            }
            else
            {
                throw new Exception($"option with name '{entity.Name}' already exists ");
            }
        }

        public async Task<bool> DeleteAsync(List<int> ids)
        {
            if (ids.Count > 0)
            {
                return await _optionRepository.DeleteAsync(ids);
            }
            else
            {
                throw new Exception("Given ids is empty");
            }
        }
    }
}
