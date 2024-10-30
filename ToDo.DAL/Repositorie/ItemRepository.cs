using ToDo.DAL.DataContext;
using ToDo.Models;

namespace ToDo.DAL.Repositorie
{
    public class ItemRepository : IGenericRepository<Item>
    {
        private readonly ArchitectureNLayerContext _dbContext;
        public ItemRepository(ArchitectureNLayerContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<bool> Delete(int id)
        {
            var model = _dbContext.Items.First(c => c.id == id);
            _dbContext.Items.Remove(model);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IQueryable<Item>> GetAll()
        {
            var query = _dbContext.Items;
            return query;
        }

        public Task<Item> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(Item entity)
        {
            await _dbContext.Items.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Item entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
