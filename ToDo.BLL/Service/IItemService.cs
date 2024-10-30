using ToDo.Models;

namespace ToDo.BLL.Service
{
    public interface IItemService
    {
        Task<bool> Insert(Item entity);
        Task<bool> Update(Item entity);
        Task<bool> Delete(int id);
        Task<Item> GetById(int id);
        Task<IQueryable<Item>> GetAll();
        Task<Item> GetByName(string name);
    }
}
