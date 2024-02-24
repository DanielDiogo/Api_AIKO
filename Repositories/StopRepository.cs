using Microsoft.EntityFrameworkCore;
using Api_AIKO.Data;
using Api_AIKO.Interfaces;
using Api_AIKO.Models;

namespace Api_AIKO.Repositories
{
    public class StopRepository : IStopRepository
    {
        private readonly ApplicationDbContext _context;

        public StopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stop>> GetAllAsync()
        {
            return await _context.stops.ToListAsync();
        }

        public async Task<Stop> GetByIdAsync(long id)
        {
            return await _context.stops.FindAsync(id);
        }

        public async Task<Stop> CreateAsync(Stop stop)
        {
            _context.stops.Add(stop);
            await _context.SaveChangesAsync();
            return stop;
        }

        public async Task UpdateAsync(Stop stop)
        {
            _context.Entry(stop).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var stop = await _context.stops.FindAsync(id);
            if (stop != null)
            {
                _context.stops.Remove(stop);
                await _context.SaveChangesAsync();
            }
        }
    }
}

