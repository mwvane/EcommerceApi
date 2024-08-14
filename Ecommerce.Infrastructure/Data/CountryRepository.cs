﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data
{
    public class CountryRepository : ICountryRepository
    {
        private readonly Context _context;
        public CountryRepository(Context context) 
        { 
            _context = context;
        }
        public async Task<Country?> AddAsync(Country entity)
        {
            try
            {
                await _context.Countries.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("an error ocured during adding an option");
            };
        }

        public Task<bool> CountryNameExistsAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            var data = await _context.Countries.FirstOrDefaultAsync(o => o.Id == id);
            return data;
        }

        public async Task<bool> UpdateAsync(Country entity)
        {
            try
            {
                _context.Countries.Update(entity);
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
