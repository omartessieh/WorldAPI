using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WorldAPI.Data;
using WorldAPI.Interfaces;

namespace WorldAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await _context.AddAsync(entity);
            await Save();
        }

        public async Task Delete(T entity)
        {
            _context.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool IsRecordExist(Expression<Func<T, bool>> condition)
        {
            var result = _context.Set<T>().AsQueryable().Where(condition).Any();
            return result;
        }
    }
}