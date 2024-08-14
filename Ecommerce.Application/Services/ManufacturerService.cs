using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class ManufacturerService : ICrudService<Manufacturer>
    {
        private readonly IManufacturerRepository _manuffacurerRepository;
        public ManufacturerService(IManufacturerRepository manufacturerRepository) 
        {
            _manuffacurerRepository = manufacturerRepository;
        }
        public Task<Manufacturer?> AddAsync(Manufacturer entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Manufacturer>> GetAllAsync()
        {
            return await _manuffacurerRepository.GetAllAsync();
        }

        public Task<Manufacturer?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Manufacturer entity)
        {
            throw new NotImplementedException();
        }
    }
}
