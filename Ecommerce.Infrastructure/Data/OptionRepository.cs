using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Data
{
    public class OptionRepository : IOptionRepository
    {
        private readonly Context _context;

        public OptionRepository(Context context)
        {
            _context = context;
        }

        public async Task<Option?> AddAsync(Option entity)
        {
            try
            {
                await _context.Options.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("an error ocured during adding an option");
            };
        }


        public async Task<bool> DeleteAsync(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var option = await _context.Options.Where(o => o.Id == id).FirstOrDefaultAsync();
                    if (option != null)
                    {
                        _context.Options.Remove(option);
                    }
                    else
                    {
                        throw new Exception($"option with ID - '{id}' not found");
                    }

                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception($"An error occured! options couldn't deleted. Try again");
            }
        }

        public async Task<ICollection<Option>> GetAllAsync()
        {
            var data = await _context.Options.Include(o => o.OptionType).ToListAsync();
            return data;
        }

        public async Task<Option?> GetByIdAsync(int id)
        {
            var data = await _context.Options.FirstOrDefaultAsync(o => o.Id == id);
            return data;
        }

        public async Task<bool> UpdateAsync(Option entity)
        {
            try
            {
                _context.Options.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("an error occurred");
            }
        }

        public async Task<bool> OptionNameExistsAsync(string name)
        {
            return await _context.Options.AnyAsync(o =>o.Name == name);
        }
    }
}
