using Microsoft.AspNetCore.Mvc;
using Api_AIKO.Interfaces;
using Api_AIKO.Models;

namespace Api_AIKO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclePositionController : ControllerBase
    {
        private readonly IVehiclePositionRepository _vehiclePositionService;

        public VehiclePositionController(IVehiclePositionRepository vehiclePositionService)
        {
            _vehiclePositionService = vehiclePositionService;
        }

        [HttpGet("{vehicleId}")]
        public async Task<ActionResult<VehiclePosition>> GetByVehicleIdAsync(long vehicleId)
        {
            var vehiclePosition = await _vehiclePositionService.GetByVehicleIdAsync(vehicleId);
            if (vehiclePosition == null)
            {
                return NotFound();
            }
            return vehiclePosition;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehiclePosition>>> GetAllAsync()
        {
            var vehiclePositions = await _vehiclePositionService.GetAllAsync();
            return vehiclePositions;
        }

        [HttpPost]
        public async Task<ActionResult<VehiclePosition>> CreateAsync(VehiclePosition vehiclePosition)
        {
            try
            {
                var createdVehiclePosition = await _vehiclePositionService.CreateAsync(vehiclePosition);
                return CreatedAtAction(nameof(GetByVehicleIdAsync), new { vehicleId = createdVehiclePosition.VehicleId }, createdVehiclePosition);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(VehiclePosition vehiclePosition)
        {
            try
            {
                await _vehiclePositionService.UpdateAsync(vehiclePosition);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{vehicleId}")]
        public async Task<IActionResult> DeleteByVehicleIdAsync(long vehicleId)
        {
            try
            {
                await _vehiclePositionService.DeleteByVehicleIdAsync(vehicleId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
