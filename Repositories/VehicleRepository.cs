using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api_AIKO.Data;
using Api_AIKO.Interfaces;
using Api_AIKO.Models;

namespace Api_AIKO.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetByIdAsync(long id)
        {
            return await _context.vehicles.FindAsync(id);
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await _context.vehicles.ToListAsync();
        }

        public async Task<Vehicle> CreateAsync(Vehicle vehicle)
        {
            _context.vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.Entry(vehicle).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var vehicle = await _context.vehicles.FindAsync(id);
            if (vehicle == null)
            {
                throw new ArgumentException("Vehicle not found.");
            }

            _context.vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
