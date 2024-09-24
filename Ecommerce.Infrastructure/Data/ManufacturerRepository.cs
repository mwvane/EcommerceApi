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
        private readonly Context _context;
        public ManufacturerRepository(Context context)
        {
            _context = context;
        }
        public async  Task<Manufacturer?> AddAsync(Manufacturer entity)
        {
            try
            {
                await _context.Manufacturers.AddAsync(entity);
                await _context.SaveChangesAsync();
                return await GetByIdAsync(entity.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error ocurred during create Manufacturer");
            }
           
        }

        public async Task<bool> DeleteAsync(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var manufacturer = await _context.Manufacturers.Where(m => m.Id == id).FirstOrDefaultAsync();
                    if (manufacturer != null)
                    {
                        _context.Manufacturers.Remove(manufacturer);
                    }
                    else
                    {
                        throw new Exception($"manufacturer with ID - '{id}' not found");
                    }

                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occured! manufacturer couldn't deleted. Try again");
            }
        }

        public async Task<ICollection<Manufacturer>> GetAllAsync()
        {
            return await _context.Manufacturers.Include(c => c.Country).ToListAsync();
        }

        public async Task<Manufacturer?> GetByIdAsync(int id)
        {
            return await _context.Manufacturers.Include(m => m.Country).Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> ManufacturerNameExistsAsync(string name)
        {
            return await _context.Manufacturers.AnyAsync(o => o.Name == name);
        }

        public async Task<bool> UpdateAsync(Manufacturer entity)
        {
            try
            {
                _context.Manufacturers.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("an error occurred");
            }
        }
    }
}
