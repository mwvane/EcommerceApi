using EcommerceApp.Controllers;
using EcommerceApp.Interfaces;
using EcommerceApp.Models.DTO;
using EcommerceApp.Validations;

namespace EcommerceApp.Services
{
    public class OptionTypeService : IOptionTypeService
    {
        private readonly IOptionTypeRepository _optionTypeRepository;

        public OptionTypeService(IOptionTypeRepository optionTypeRepository)
        {
            _optionTypeRepository = optionTypeRepository;
        }

        public async Task<OptionTypeDto> AddItemAsync(OptionTypeDto item, CRUD_Action action = CRUD_Action.Create)
        {
           return await _optionTypeRepository.AddAsync(item, action);
        }

        public async Task DeleteItemAsync(List<int> ids)
        {
           await _optionTypeRepository.DeleteAsync(ids);
        }

        public async Task<IEnumerable<OptionTypeDto>> GetAllItemsAsync()
        {
            return await _optionTypeRepository.GetAllAsync();
        }

        public async Task<OptionTypeDto> GetItemByIdAsync(int id)
        {
            return await GetItemByIdAsync(id);
        }

        public async Task UpdateItemAsync(OptionTypeDto item)
        {
            await _optionTypeRepository.UpdateAsync(item);
        }
    }
}
