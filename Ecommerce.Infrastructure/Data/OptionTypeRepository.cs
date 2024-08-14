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
    public class OptionTypeRepository : IOptionTypeRepository
    {
        private readonly Context _context;

        public OptionTypeRepository(Context context)
        {
            _context = context;
        }
        public async Task<OptionType?> AddAsync(OptionType entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var optionType = await _context.OptionsTypes.Where(o => o.Id == id).FirstOrDefaultAsync();
                    if (optionType != null)
                    {
                        _context.OptionsTypes.Remove(optionType);
                    }
                    else
                    {
                        throw new Exception($"option type with ID - '{id}' not found");
                    }

                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occured! option types couldn't deleted. Try again");
            }
        }

        public async Task<ICollection<OptionType>> GetAllAsync()
        {
            return await _context.OptionsTypes.ToListAsync();
        }

        public async Task<OptionType?> GetByIdAsync(int id)
        {
            var data = await _context.OptionsTypes.FirstOrDefaultAsync(o => o.Id == id);
            return data;
        }

        public async Task<bool> OptionTypeNameExistsAsync(string name)
        {
            return await _context.OptionsTypes.AnyAsync(o => o.Name == name);
        }

        public async Task<bool> UpdateAsync(OptionType entity)
        {
            try
            {
                _context.OptionsTypes.Update(entity);
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
