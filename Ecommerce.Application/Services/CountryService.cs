using Ecommerce.Application.Helpers;
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
            return _countryRepository.AddAsync(entity);
        }

        public Task<bool> DeleteAsync(List<int> ids)
        {
           return (_countryRepository.DeleteAsync(ids));
        }

        public async Task<ICollection<Country>> GetAllAsync()
        {
            var host = HostHeplers.url;
            var countries = await _countryRepository.GetAllAsync();
            foreach(var country in countries)
            {
                if(country.Image != null)
                {
                    country.Image = $"{host}/{country.Image}";
                }
            }
            return countries.OrderBy(x => x.Name).ToList();
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
