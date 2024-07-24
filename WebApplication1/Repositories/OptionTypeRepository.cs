using EcommerceApp.Controllers;
using EcommerceApp.Data;
using EcommerceApp.ErrorHandling;
using EcommerceApp.Interfaces;
using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using EcommerceApp.Validations;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories
{
    public class OptionTypeRepository : IOptionTypeRepository
    {
        private readonly Context _context;
        public OptionTypeRepository(Context context) { _context = context; }

        public async Task<OptionTypeDto> AddAsync(OptionTypeDto entity, CRUD_Action action = CRUD_Action.Create)
        {
            var exist = await ItemValidations.IsItemNameExist<OptionType>(_context, entity.Name);
            if(exist)
            {
                throw new Exception($"option type with name {entity.Name} already exists");
            }
            var optionType = new OptionType()
            {
                OptionTypeId = entity.Id,
                Name = entity.Name,
            };
            if(action == CRUD_Action.Create)
            {
                var newOptionType = await _context.OptionsTypes.AddAsync(optionType);
                entity.Id = optionType.OptionTypeId;
            }
            else if(action == CRUD_Action.Update) {
                _context.OptionsTypes.Update(optionType);
            }

            await _context.SaveChangesAsync();
            return entity;
        }

        public Task DeleteAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OptionTypeDto>> GetAllAsync()
        {
            try
            {
                var data = _context.OptionsTypes.Select(o => new OptionTypeDto
                {
                    Id = o.OptionTypeId,
                    Name = o.Name,
                });
                return data;
            }
            catch (Exception ex)
            {
                throw new NotFoundException("option types not found");
            }

        }

        public async Task<OptionTypeDto> GetByIdAsync(int id)
        {
            var optionType = await _context.OptionsTypes.
                Select(o => new OptionTypeDto
                {
                    Name = o.Name,
                    Id = o.OptionTypeId,
                }).SingleOrDefaultAsync(o => o.Id == id);
            if (optionType != null)
            {
                return optionType;
            }
            throw new NotFoundException($"option type with ID - {id} not found");
        }

        public Task UpdateAsync(OptionTypeDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
