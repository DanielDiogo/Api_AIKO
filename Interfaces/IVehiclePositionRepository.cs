using Api_AIKO.Models;

namespace Api_AIKO.Interfaces
{
    public interface IVehiclePositionRepository
    {
        Task<VehiclePosition> GetByVehicleIdAsync(long vehicleId);
        Task<List<VehiclePosition>> GetAllAsync();
        Task<VehiclePosition> CreateAsync(VehiclePosition vehiclePosition);
        Task UpdateAsync(VehiclePosition vehiclePosition);
        Task DeleteByVehicleIdAsync(long vehicleId);
    }
}
