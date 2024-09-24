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
        public async Task<Manufacturer?> AddAsync(Manufacturer entity)
        {
            if (!await _manuffacurerRepository.ManufacturerNameExistsAsync(entity.Name))
            {
                return await _manuffacurerRepository.AddAsync(entity);
            }
            throw new Exception($"Manufacturer with name '{entity.Name}' already exist");
        }

        public async Task<bool> DeleteAsync(List<int> ids)
        {
            if (ids.Count > 0)
            {
                return await _manuffacurerRepository.DeleteAsync(ids);
            }
            else
            {
                throw new Exception("Given ids is empty");
            }
        }

        public async Task<ICollection<Manufacturer>> GetAllAsync()
        {
            return await _manuffacurerRepository.GetAllAsync();
        }

        public async Task<Manufacturer?> GetByIdAsync(int id)
        {
            return  await _manuffacurerRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Manufacturer entity)
        {
            return await _manuffacurerRepository.UpdateAsync(entity);
        }
    }
}
