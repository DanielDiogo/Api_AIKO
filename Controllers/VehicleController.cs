using Microsoft.AspNetCore.Mvc;
using Api_AIKO.Interfaces;
using Api_AIKO.Models;

namespace Api_AIKO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleService;

        public VehicleController(IVehicleRepository vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetById(long id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return vehicle;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vehicle>>> GetAll()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return vehicles;
        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> Create(Vehicle vehicle)
        {
            try
            {
                var createdVehicle = await _vehicleService.CreateAsync(vehicle);
                return CreatedAtAction(nameof(GetById), new { id = createdVehicle.Id }, createdVehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest("O ID fornecido na URL não corresponde ao ID do objeto.");
            }

            try
            {
                await _vehicleService.UpdateAsync(vehicle);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _vehicleService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
