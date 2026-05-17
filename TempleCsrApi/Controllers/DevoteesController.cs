using Microsoft.AspNetCore.Mvc;
using TempleCsrApi.Models;
using TempleCsrApi.Services;

namespace TempleCsrApi.Controllers
{
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
        public async Task<IActionResult> GetAllDevotees()
        {
            var devotees = await _devoteeService.GetAllAsync();

            return Ok(devotees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevoteeById(int id)
        {
            var devotee = await _devoteeService.GetByIdAsync(id);

            if (devotee == null)
            {
                return NotFound("Devotee not found");
            }

            return Ok(devotee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevotee(Devotee devotee)
        {
            var createdDevotee = await _devoteeService.CreateAsync(devotee);

            if (createdDevotee == null)
            {
                return BadRequest("Phone number already exists");
            }

            return CreatedAtAction(
                nameof(GetDevoteeById),
                new { id = createdDevotee.Id },
                createdDevotee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevotee(int id, Devotee devotee)
        {
            var updatedDevotee = await _devoteeService.UpdateAsync(id, devotee);

            if (updatedDevotee == null)
            {
                return NotFound("Devotee not found");
            }

            return Ok(updatedDevotee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevotee(int id)
        {
            var result = await _devoteeService.DeleteAsync(id);

            if (result == false)
            {
                return NotFound("Devotee not found");
            }

            return Ok("Devotee deleted successfully");
        }
    }
}