using Microsoft.AspNetCore.Mvc;
using Api_AIKO.Interfaces;
using Api_AIKO.Models;
using Microsoft.AspNetCore.Authorization;

namespace Api_AIKO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LineController : ControllerBase 
    {
        private readonly ILineRepository _lineService;

    public LineController(ILineRepository lineService)
    {
        _lineService = lineService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Line>> GetById(long id)
    {
        var line = await _lineService.GetByIdAsync(id);
        if (line == null)
        {
            return NotFound();
        }
        return line;
    }

    [HttpGet]
    public async Task<ActionResult<List<Line>>> GetAll()
    {
        var lines = await _lineService.GetAllAsync();
        return lines;
    }

    [HttpPost]
    public async Task<ActionResult<Line>> Create(Line line)
    {
        try
        {
            var createdLine = await _lineService.CreateAsync(line);
            return CreatedAtAction(nameof(GetById), new { id = createdLine.Id }, createdLine);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Line line)
    {
        if (id != line.Id)
        {
            return BadRequest("O ID fornecido na URL não corresponde ao ID do objeto.");
        }

        try
        {
            await _lineService.UpdateAsync(line);
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
            await _lineService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

}
