using WorldAPI.Entities;

namespace WorldAPI.Interfaces
{
    public interface IStatesRepository : IGenericRepository<States>
    {
        Task Update(States entity);
    }
}