using WorldAPI.Entities;
using WorldAPI.Repository;

namespace WorldAPI.Interfaces
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task Update(Country entity);
    }
}