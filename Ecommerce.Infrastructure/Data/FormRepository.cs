using Ecommerce.Core.Entities.FormData;
using Ecommerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data
{
    public class FormRepository : IFormRepository
    {
        private readonly Context _context;
        public FormRepository(Context context)
        {
            _context = context;
        }

        public Task<Form?> AddAsync(Form entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Form>> GetAllAsync()
        {
            var formWithDetails = await _context.Forms.ToListAsync();
                
                    
                
                 // Load radio button options for each form control
            return formWithDetails;
        }

        public async Task<Form?> GetByIdAsync(int id)
        {
            var formWithDetails = await _context.Forms
             .Include(f => f.Sections)
                 .ThenInclude(s => s.FormControls)
                     .ThenInclude(fc => fc.Validators)
             .Include(f => f.Sections)
                 .ThenInclude(s => s.FormControls)
                     .ThenInclude(fc => fc.AdditionalLinks)
             .Include(f => f.Sections)
                 .ThenInclude(s => s.FormControls)
                     .ThenInclude(fc => fc.Options)
             .FirstOrDefaultAsync(f => f.Id == id);
            return formWithDetails;
        }

        public Task<bool> UpdateAsync(Form entity)
        {
            throw new NotImplementedException();
        }
    }
}
