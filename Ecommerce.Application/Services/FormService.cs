using Ecommerce.Core.Entities.FormData;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class FormService : ICrudService<Form>
    {
        private readonly IFormRepository _formRepository;
        public FormService(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }
        public Task<Form?> AddAsync(Form entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Form>> GetAllAsync()
        {
            return _formRepository.GetAllAsync();
        }

        public Task<Form?> GetByIdAsync(int id)
        {
           return _formRepository.GetByIdAsync(id);
        }

        public Task<bool> UpdateAsync(Form entity)
        {
            throw new NotImplementedException();
        }
    }
}
