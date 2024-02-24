using Microsoft.EntityFrameworkCore;
using Api_AIKO.Data;
using Api_AIKO.Interfaces;
using Api_AIKO.Models;

namespace Api_AIKO.Repositories
{
    public class VehiclePositionRepository : IVehiclePositionRepository
    {
        private readonly ApplicationDbContext _context;

        public VehiclePositionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VehiclePosition> GetByIdAsync(long id)
        {
            return await _context.vehiclesPositions.FindAsync(id);
        }

        public async Task<List<VehiclePosition>> GetAllAsync()
        {
            return await _context.vehiclesPositions.ToListAsync();
        }

        public async Task<VehiclePosition> CreateAsync(VehiclePosition vehiclePosition)
        {
            _context.vehiclesPositions.Add(vehiclePosition);
            await _context.SaveChangesAsync();
            return vehiclePosition;
        }

        public async Task UpdateAsync(VehiclePosition vehiclePosition)
        {
            _context.Entry(vehiclePosition).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var vehiclePosition = await _context.vehiclesPositions.FindAsync(id);
            if (vehiclePosition == null)
            {
                throw new ArgumentException("Vehicle position not found.");
            }

            _context.vehiclesPositions.Remove(vehiclePosition);
            await _context.SaveChangesAsync();
        }

        public async Task<VehiclePosition> GetByVehicleIdAsync(long vehicleId)
        {
            return await _context.vehiclesPositions.FirstOrDefaultAsync(vp => vp.VehicleId == vehicleId);
        }

        public async Task DeleteByVehicleIdAsync(long vehicleId)
        {
            var positions = await _context.vehiclesPositions.Where(vp => vp.VehicleId == vehicleId).ToListAsync();
            _context.vehiclesPositions.RemoveRange(positions);
            await _context.SaveChangesAsync();
        }
    }
}
