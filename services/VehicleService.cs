using Api_AIKO.Interfaces;
namespace Api_AIKO.Controllers
{
    public class VehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        // Implement CRUD operations here...
    }
}
