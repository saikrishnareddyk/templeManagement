using Microsoft.AspNetCore.Mvc;
using TempleManagementApi.DTOs;
using TempleManagementApi.Helpers;
using TempleManagementApi.Interfaces;

namespace TempleManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DonationsController : ControllerBase
{
    private readonly IDonationService _donationService;

    public DonationsController(IDonationService donationService)
    {
        _donationService = donationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DonationDto>>> GetAllDonations()
    {
        var donations = await _donationService.GetAllDonationsAsync();

        return Ok(donations);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DonationDto>> GetDonationById(int id)
    {
        var donation = await _donationService.GetDonationByIdAsync(id);

        if (donation == null)
        {
            return NotFound($"Donation with Id {id} was not found.");
        }

        return Ok(donation);
    }

    [HttpPost]
    public async Task<ActionResult<DonationDto>> CreateDonation(CreateDonationDto createDonationDto)
    {
        if (!PaymentModeHelper.IsValidPaymentMode(createDonationDto.PaymentMode))
        {
            return BadRequest(
                $"Invalid payment mode. Allowed payment modes are: {string.Join(", ", PaymentModeHelper.AllowedPaymentModes)}"
            );
        }

        var createdDonation = await _donationService.CreateDonationAsync(createDonationDto);

        if (createdDonation == null)
        {
            return BadRequest("Invalid DevoteeId or PaymentMode. Please provide valid details.");
        }

        return CreatedAtAction(
            nameof(GetDonationById),
            new { id = createdDonation.Id },
            createdDonation
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<DonationDto>> UpdateDonation(
        int id,
        UpdateDonationDto updateDonationDto)
    {
        if (!PaymentModeHelper.IsValidPaymentMode(updateDonationDto.PaymentMode))
        {
            return BadRequest(
                $"Invalid payment mode. Allowed payment modes are: {string.Join(", ", PaymentModeHelper.AllowedPaymentModes)}"
            );
        }

        var updatedDonation = await _donationService.UpdateDonationAsync(
            id,
            updateDonationDto
        );

        if (updatedDonation == null)
        {
            return NotFound($"Donation with Id {id} was not found.");
        }

        return Ok(updatedDonation);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDonation(int id)
    {
        var isDeleted = await _donationService.DeleteDonationAsync(id);

        if (!isDeleted)
        {
            return NotFound($"Donation with Id {id} was not found.");
        }

        return NoContent();
    }
}