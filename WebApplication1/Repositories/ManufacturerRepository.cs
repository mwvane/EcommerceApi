using EcommerceApp.Controllers;
using EcommerceApp.Data;
using EcommerceApp.Interfaces;
using EcommerceApp.Models.DTO;

namespace EcommerceApp.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly Context _context;
        public ManufacturerRepository(Context context) { _context = context; }

        public Task<ManufacturerDto> AddAsync(ManufacturerDto entity, CRUD_Action action = CRUD_Action.Create)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ManufacturerDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ManufacturerDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ManufacturerDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
