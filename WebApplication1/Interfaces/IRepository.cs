using EcommerceApp.Controllers;

namespace EcommerceApp.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity, CRUD_Action action = CRUD_Action.Create);
        Task UpdateAsync(T entity);
        Task DeleteAsync(List<int> ids);
    }
}
