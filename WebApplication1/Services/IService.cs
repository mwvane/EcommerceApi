using EcommerceApp.Controllers;
using EcommerceApp.Interfaces;

namespace EcommerceApp.Services
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllItemsAsync();
        Task<T> GetItemByIdAsync(int id);
        Task<T> AddItemAsync(T item, CRUD_Action action = CRUD_Action.Create);
        Task UpdateItemAsync(T item);
        Task DeleteItemAsync(List<int> ids);
    }
}
