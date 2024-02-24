using Microsoft.AspNetCore.Mvc;
using Api_AIKO.Interfaces;
using Api_AIKO.Models;

namespace Api_AIKO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StopController : ControllerBase
    {
        private readonly IStopRepository _stopService;

        public StopController(IStopRepository stopService)
        {
            _stopService = stopService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stop>> GetById(long id)
        {
            var stop = await _stopService.GetByIdAsync(id);
            if (stop == null)
            {
                return NotFound();
            }
            return stop;
        }

        [HttpGet]
        public async Task<ActionResult<List<Stop>>> GetAll()
        {
            var stops = await _stopService.GetAllAsync();
            return stops;
        }

        [HttpPost]
        public async Task<ActionResult<Stop>> Create(Stop stop)
        {
            try
            {
                var createdStop = await _stopService.CreateAsync(stop);
                return CreatedAtAction(nameof(GetById), new { id = createdStop.Id }, createdStop);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Stop stop)
        {
            if (id != stop.Id)
            {
                return BadRequest("O ID fornecido na URL não corresponde ao ID do objeto.");
            }

            try
            {
                await _stopService.UpdateAsync(stop);
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
                await _stopService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
