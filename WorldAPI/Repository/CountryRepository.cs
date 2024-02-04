using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WorldAPI.Data;
using WorldAPI.Entities;
using WorldAPI.Interfaces;

namespace WorldAPI.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Country entity)
        {
            _context.Countries.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}