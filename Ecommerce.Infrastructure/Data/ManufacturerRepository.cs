using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Data
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly Context _contex;
        public ManufacturerRepository(Context context)
        {
            _contex = context;
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
            return await _contex.Manufacturers.Include(c => c.Country).ToListAsync();
        }

        public Task<Manufacturer?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ManufacturerNameExistsAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Manufacturer entity)
        {
            throw new NotImplementedException();
        }
    }
}
