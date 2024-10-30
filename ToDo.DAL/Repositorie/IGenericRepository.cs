namespace ToDo.DAL.Repositorie
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        Task<bool> Insert(TEntityModel entity);
        Task<bool> Update(TEntityModel entity);
        Task<bool> Delete(int id);
        Task<TEntityModel> GetById(int id);
        Task<IQueryable<TEntityModel>> GetAll();
    }
}
