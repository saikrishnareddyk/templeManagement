using Microsoft.AspNetCore.Mvc;
using TempleManagementApi.DTOs;
using TempleManagementApi.Interfaces;
using TempleManagementApi.Responses;

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
    public async Task<ActionResult<ApiResponse<List<DevoteeDto>>>> GetAllDevotees()
    {
        var devotees = await _devoteeService.GetAllDevoteesAsync();

        return Ok(ApiResponse<List<DevoteeDto>>.SuccessResponse(
            devotees,
            "Devotees fetched successfully"
        ));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<DevoteeDto>>> GetDevoteeById(int id)
    {
        var devotee = await _devoteeService.GetDevoteeByIdAsync(id);

        if (devotee == null)
        {
            return NotFound(ApiResponse<DevoteeDto>.FailureResponse(
                "Devotee not found",
                new List<string> { $"Devotee with Id {id} was not found" }
            ));
        }

        return Ok(ApiResponse<DevoteeDto>.SuccessResponse(
            devotee,
            "Devotee fetched successfully"
        ));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<DevoteeDto>>> CreateDevotee(CreateDevoteeDto createDevoteeDto)
    {
        var createdDevotee = await _devoteeService.CreateDevoteeAsync(createDevoteeDto);

        return CreatedAtAction(
            nameof(GetDevoteeById),
            new { id = createdDevotee.Id },
            ApiResponse<DevoteeDto>.SuccessResponse(
                createdDevotee,
                "Devotee created successfully"
            )
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<DevoteeDto>>> UpdateDevotee(
        int id,
        UpdateDevoteeDto updateDevoteeDto)
    {
        var updatedDevotee = await _devoteeService.UpdateDevoteeAsync(id, updateDevoteeDto);

        if (updatedDevotee == null)
        {
            return NotFound(ApiResponse<DevoteeDto>.FailureResponse(
                "Devotee not found",
                new List<string> { $"Devotee with Id {id} was not found" }
            ));
        }

        return Ok(ApiResponse<DevoteeDto>.SuccessResponse(
            updatedDevotee,
            "Devotee updated successfully"
        ));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDevotee(int id)
    {
        var isDeleted = await _devoteeService.DeleteDevoteeAsync(id);

        if (!isDeleted)
        {
            return NotFound(ApiResponse<object>.FailureResponse(
                "Devotee not found",
                new List<string> { $"Devotee with Id {id} was not found" }
            ));
        }

        return Ok(ApiResponse<object>.SuccessResponse(
            null,
            "Devotee deleted successfully"
        ));
    }
}