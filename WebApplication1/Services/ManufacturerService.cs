using EcommerceApp.Controllers;
using EcommerceApp.Models.DTO;

namespace EcommerceApp.Services
{
    public class ManufacturerService : IManufacturerService
    {
        public Task<ManufacturerDto> AddItemAsync(ManufacturerDto item, CRUD_Action action = CRUD_Action.Create)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItemAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ManufacturerDto>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ManufacturerDto> GetItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemAsync(ManufacturerDto item)
        {
            throw new NotImplementedException();
        }
    }
}
