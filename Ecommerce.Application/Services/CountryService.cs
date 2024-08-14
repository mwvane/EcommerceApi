using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public  class CountryService: ICrudService<Country>
    {
        private readonly ICountryRepository _countryRepository;
        public CountryService(ICountryRepository countryRepository)
        { 
            _countryRepository = countryRepository; 
        }

        public Task<Country?> AddAsync(Country entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Country>> GetAllAsync()
        {
            return await _countryRepository.GetAllAsync();
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            return await _countryRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Country entity)
        {
            return await _countryRepository.UpdateAsync(entity);
        }
    }
}
