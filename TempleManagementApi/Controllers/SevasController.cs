using Microsoft.AspNetCore.Mvc;
using TempleManagementApi.DTOs;
using TempleManagementApi.Interfaces;

namespace TempleManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SevasController : ControllerBase
{
    private readonly ISevaService _sevaService;

    public SevasController(ISevaService sevaService)
    {
        _sevaService = sevaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SevaDto>>> GetAllSevas()
    {
        var sevas = await _sevaService.GetAllSevasAsync();

        return Ok(sevas);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SevaDto>> GetSevaById(int id)
    {
        var seva = await _sevaService.GetSevaByIdAsync(id);

        if (seva == null)
        {
            return NotFound($"Seva with Id {id} was not found.");
        }

        return Ok(seva);
    }

    [HttpPost]
    public async Task<ActionResult<SevaDto>> CreateSeva(CreateSevaDto createSevaDto)
    {
        var createdSeva = await _sevaService.CreateSevaAsync(createSevaDto);

        return CreatedAtAction(
            nameof(GetSevaById),
            new { id = createdSeva.Id },
            createdSeva
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<SevaDto>> UpdateSeva(
        int id,
        UpdateSevaDto updateSevaDto)
    {
        var updatedSeva = await _sevaService.UpdateSevaAsync(id, updateSevaDto);

        if (updatedSeva == null)
        {
            return NotFound($"Seva with Id {id} was not found.");
        }

        return Ok(updatedSeva);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSeva(int id)
    {
        var isDeleted = await _sevaService.DeleteSevaAsync(id);

        if (!isDeleted)
        {
            return BadRequest($"Seva with Id {id} was not found or it already has booking records.");
        }

        return NoContent();
    }
}