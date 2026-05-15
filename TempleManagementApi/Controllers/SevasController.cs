using Microsoft.AspNetCore.Mvc;
using TempleManagementApi.DTOs;
using TempleManagementApi.Interfaces;
using TempleManagementApi.Responses;

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
    public async Task<ActionResult<ApiResponse<List<SevaDto>>>> GetAllSevas()
    {
        var sevas = await _sevaService.GetAllSevasAsync();

        return Ok(ApiResponse<List<SevaDto>>.SuccessResponse(
            sevas,
            "Sevas fetched successfully"
        ));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<SevaDto>>> GetSevaById(int id)
    {
        var seva = await _sevaService.GetSevaByIdAsync(id);

        if (seva == null)
        {
            return NotFound(ApiResponse<SevaDto>.FailureResponse(
                "Seva not found",
                new List<string> { $"Seva with Id {id} was not found" }
            ));
        }

        return Ok(ApiResponse<SevaDto>.SuccessResponse(
            seva,
            "Seva fetched successfully"
        ));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<SevaDto>>> CreateSeva(CreateSevaDto createSevaDto)
    {
        var createdSeva = await _sevaService.CreateSevaAsync(createSevaDto);

        return CreatedAtAction(
            nameof(GetSevaById),
            new { id = createdSeva.Id },
            ApiResponse<SevaDto>.SuccessResponse(
                createdSeva,
                "Seva created successfully"
            )
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<SevaDto>>> UpdateSeva(
        int id,
        UpdateSevaDto updateSevaDto)
    {
        var updatedSeva = await _sevaService.UpdateSevaAsync(id, updateSevaDto);

        if (updatedSeva == null)
        {
            return NotFound(ApiResponse<SevaDto>.FailureResponse(
                "Seva not found",
                new List<string> { $"Seva with Id {id} was not found" }
            ));
        }

        return Ok(ApiResponse<SevaDto>.SuccessResponse(
            updatedSeva,
            "Seva updated successfully"
        ));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSeva(int id)
    {
        var isDeleted = await _sevaService.DeleteSevaAsync(id);

        if (!isDeleted)
        {
            return BadRequest(ApiResponse<object>.FailureResponse(
                "Seva cannot be deleted",
                new List<string>
                {
                    $"Seva with Id {id} was not found or it already has booking records"
                }
            ));
        }

        return Ok(ApiResponse<object>.SuccessResponse(
            null,
            "Seva deleted successfully"
        ));
    }
}