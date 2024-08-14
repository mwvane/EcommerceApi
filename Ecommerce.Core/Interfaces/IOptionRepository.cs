using Ecommerce.Core.Entities;

namespace Ecommerce.Core.Interfaces
{
    public interface IOptionRepository : IRepository<Option>
    {
        Task<bool> OptionNameExistsAsync(string name);
    }
}
