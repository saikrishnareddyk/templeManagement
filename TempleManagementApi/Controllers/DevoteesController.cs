using Microsoft.AspNetCore.Mvc;
using TempleManagementApi.DTOs;
using TempleManagementApi.Interfaces;

namespace TempleManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevoteesController : ControllerBase
{
    private readonly IDevoteeService _devoteeService;

    public DevoteesController(IDevoteeService devoteeService)
    {
        _devoteeService = devoteeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DevoteeDto>>> GetAllDevotees()
    {
        var devotees = await _devoteeService.GetAllDevoteesAsync();

        return Ok(devotees);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DevoteeDto>> GetDevoteeById(int id)
    {
        var devotee = await _devoteeService.GetDevoteeByIdAsync(id);

        if (devotee == null)
        {
            return NotFound($"Devotee with Id {id} was not found.");
        }

        return Ok(devotee);
    }

    [HttpPost]
    public async Task<ActionResult<DevoteeDto>> CreateDevotee(CreateDevoteeDto createDevoteeDto)
    {
        var createdDevotee = await _devoteeService.CreateDevoteeAsync(createDevoteeDto);

        return CreatedAtAction(
            nameof(GetDevoteeById),
            new { id = createdDevotee.Id },
            createdDevotee
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<DevoteeDto>> UpdateDevotee(
        int id,
        UpdateDevoteeDto updateDevoteeDto)
    {
        var updatedDevotee = await _devoteeService.UpdateDevoteeAsync(id, updateDevoteeDto);

        if (updatedDevotee == null)
        {
            return NotFound($"Devotee with Id {id} was not found.");
        }

        return Ok(updatedDevotee);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDevotee(int id)
    {
        var isDeleted = await _devoteeService.DeleteDevoteeAsync(id);

        if (!isDeleted)
        {
            return NotFound($"Devotee with Id {id} was not found.");
        }

        return NoContent();
    }
}