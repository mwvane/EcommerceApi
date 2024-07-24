using EcommerceApp.Controllers;
using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using EcommerceApp.Repositories;

namespace EcommerceApp.Services
{
    public class OptionService :  IOptionService
    {
        private readonly IOptionRepository _optionRepository;

        public OptionService(IOptionRepository optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public async Task<OptionDto> AddItemAsync(OptionDto option, CRUD_Action action = CRUD_Action.Create)
        {
            return await _optionRepository.AddAsync(option);
        }
         
        public async Task DeleteItemAsync(List<int> ids)
        {
            await _optionRepository.DeleteAsync(ids);
        }

        public async Task<IEnumerable<OptionDto>> GetAllItemsAsync()
        {
            return await _optionRepository.GetAllAsync();
        }

        public async Task<OptionDto> GetItemByIdAsync(int id)
        {
            return await _optionRepository.GetByIdAsync(id);
        }

        public async Task UpdateItemAsync(OptionDto option)
        {
            await _optionRepository.UpdateAsync(option);
        }
    }
}
