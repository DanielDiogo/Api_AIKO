using Api_AIKO.Models;

namespace Api_AIKO.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetByIdAsync(long id);
        Task<List<Vehicle>> GetAllAsync();
        Task<Vehicle> CreateAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(long id);
    }
}
