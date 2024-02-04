using System.Linq.Expressions;
using WorldAPI.Entities;

namespace WorldAPI.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task Create(T entity);
        Task Delete(T entity);
        Task Save();
        bool IsRecordExist(Expression<Func<T, bool>> condition);
    }
}