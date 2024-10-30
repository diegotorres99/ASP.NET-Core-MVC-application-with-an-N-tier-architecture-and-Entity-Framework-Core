using ToDo.DAL.Repositorie;
using ToDo.Models;

namespace ToDo.BLL.Service
{
    public class ItemService : IItemService
    {
        private readonly IGenericRepository<Item> _repositoryItem;
        public ItemService(IGenericRepository<Item> repositoryItem)
        {
            _repositoryItem = repositoryItem;
        }
        public async Task<bool> Delete(int id)
        {
            return await _repositoryItem.Delete(id);
        }

        public async Task<IQueryable<Item>> GetAll()
        {
            return await _repositoryItem.GetAll();
        }

        public async Task<Item> GetById(int id)
        {
            return await _repositoryItem.GetById(id);
        }

        public async Task<Item> GetByName(string name)
        {
            IQueryable<Item> query = await _repositoryItem.GetAll();
            var item = query.Where(x => x.name == name).FirstOrDefault();
            return item;
        }

        public Task<bool> Insert(Item entity)
        {
            return _repositoryItem.Insert(entity);
        }

        public Task<bool> Update(Item entity)
        {
            return _repositoryItem.Update(entity);
        }
    }
}
