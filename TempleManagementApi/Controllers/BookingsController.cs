using Microsoft.AspNetCore.Mvc;
using TempleManagementApi.DTOs;
using TempleManagementApi.Helpers;
using TempleManagementApi.Interfaces;

namespace TempleManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookingDto>>> GetAllBookings()
    {
        var bookings = await _bookingService.GetAllBookingsAsync();

        return Ok(bookings);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookingDto>> GetBookingById(int id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);

        if (booking == null)
        {
            return NotFound($"Booking with Id {id} was not found.");
        }

        return Ok(booking);
    }

    [HttpPost]
    public async Task<ActionResult<BookingDto>> CreateBooking(CreateBookingDto createBookingDto)
    {
        if (createBookingDto.BookingDate.Date < DateTime.UtcNow.Date)
        {
            return BadRequest("Booking date cannot be in the past.");
        }

        var createdBooking = await _bookingService.CreateBookingAsync(createBookingDto);

        if (createdBooking == null)
        {
            return BadRequest("Invalid DevoteeId or SevaId. Please provide existing Devotee and Seva.");
        }

        return CreatedAtAction(
            nameof(GetBookingById),
            new { id = createdBooking.Id },
            createdBooking
        );
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<BookingDto>> UpdateBookingStatus(
        int id,
        UpdateBookingStatusDto updateBookingStatusDto)
    {
        if (!BookingStatusHelper.IsValidStatus(updateBookingStatusDto.Status))
        {
            return BadRequest(
                $"Invalid status. Allowed statuses are: {string.Join(", ", BookingStatusHelper.AllowedStatuses)}"
            );
        }

        var updatedBooking = await _bookingService.UpdateBookingStatusAsync(
            id,
            updateBookingStatusDto
        );

        if (updatedBooking == null)
        {
            return NotFound($"Booking with Id {id} was not found.");
        }

        return Ok(updatedBooking);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var isDeleted = await _bookingService.DeleteBookingAsync(id);

        if (!isDeleted)
        {
            return NotFound($"Booking with Id {id} was not found.");
        }

        return NoContent();
    }
}