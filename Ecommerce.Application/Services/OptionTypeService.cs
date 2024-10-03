using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public  class OptionTypeService : ICrudService<OptionType>
    {
        private readonly IOptionTypeRepository _optionTypeRepository;
        public OptionTypeService(IOptionTypeRepository optionTypeRepository) { _optionTypeRepository = optionTypeRepository; }
        public async Task<ICollection<OptionType>> GetAllOptionTypes()
        {
            var data = await _optionTypeRepository.GetAllAsync();
            return data.OrderBy(x => x.Name).ToList();
        }

        public Task<ICollection<OptionType>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<OptionType?> GetByIdAsync(int id)
        {
            return await _optionTypeRepository.GetByIdAsync(id);
        }

        public async Task<OptionType?> AddAsync(OptionType entity)
        {
            if (!await _optionTypeRepository.OptionTypeNameExistsAsync(entity.Name))
            {
                return await _optionTypeRepository.AddAsync(entity);
            }
            throw new Exception($"optionType wit name {entity.Name} alraedy exists");
        }

        public async Task<bool> UpdateAsync(OptionType entity)
        {
            return await _optionTypeRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(List<int> ids)
        {
            return await _optionTypeRepository.DeleteAsync(ids);
        }
    }
}
