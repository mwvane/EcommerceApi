using EcommerceApp.Controllers;
using EcommerceApp.Data;
using EcommerceApp.ErrorHandling;
using EcommerceApp.Extensions;
using EcommerceApp.Interfaces;
using EcommerceApp.Models;
using EcommerceApp.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EcommerceApp.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly Context _context;
        public OptionRepository(Context context) { _context = context; }

        public async Task<OptionDto> AddAsync(OptionDto optionDto, CRUD_Action action = CRUD_Action.Create)
        {
            var newOption = optionDto.ToOption();
            if (newOption != null)
            {
                try
                {
                    if(action == CRUD_Action.Create)
                    {
                        await _context.Options.AddAsync(newOption);
                        optionDto.OptionId = newOption.OptionId;
                    }
                    else if(action == CRUD_Action.Update)
                    {
                        _context.Options.Update(newOption);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"an error occurred during {action.ToString()} option, try again");
                }

            }

            return optionDto;
        }

        public async Task DeleteAsync(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var option = await _context.Options.FindAsync(id);
                    if (option != null)
                    {
                        _context.Options.Remove(option);
                    }
                    else
                    {
                        throw new NotFoundException($"option with ID - {id} not found");
                    }
                };
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("an error occured, try again");
            }

        }

        public async Task<IEnumerable<OptionDto>> GetAllAsync()
        {
            var data = _context.Options.Include(ot => ot.OptionType).Select(o => new OptionDto
            {
                OptionId = o.OptionId,
                Name = $"{o.Name} ({o.Value} )",
                Value = o.Value,
                OptionType = o.OptionType
            });
            return await data.ToListAsync();
        }

        public async Task<OptionDto> GetByIdAsync(int id)
        {
            var option = await _context.Options.
                Select(o => new OptionDto
                {
                    OptionId = o.OptionId,
                    Name = o.Name,
                    OptionTypeId = o.OptionTypeId,
                    Value = o.Value

                }).SingleOrDefaultAsync(c => c.OptionId == id);

            if (option != null)
            {
                return option;
            }
            throw new NotFoundException($"option with ID - {id} not found");
        }

        public async Task UpdateAsync(OptionDto entity)
        {
            await AddAsync(entity, CRUD_Action.Update);
        }
    }
}
