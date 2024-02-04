using Microsoft.EntityFrameworkCore;
using WorldAPI.Data;
using WorldAPI.Entities;
using WorldAPI.Interfaces;

namespace WorldAPI.Repository
{
    public class StatesRepository : GenericRepository<States>, IStatesRepository
    {
        private readonly ApplicationDbContext _context;

        public StatesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(States entity)
        {
            _context.States.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}