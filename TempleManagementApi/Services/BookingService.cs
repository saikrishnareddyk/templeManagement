using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TempleManagementApi.Data;
using TempleManagementApi.DTOs;
using TempleManagementApi.Helpers;
using TempleManagementApi.Interfaces;
using TempleManagementApi.Models;

namespace TempleManagementApi.Services;

public class BookingService : IBookingService
{
    private readonly TempleDbContext _context;
    private readonly IMapper _mapper;

    public BookingService(TempleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BookingDto>> GetAllBookingsAsync()
    {
        var bookings = await _context.Bookings
            .Include(b => b.Devotee)
            .Include(b => b.Seva)
            .OrderByDescending(b => b.CreatedDate)
            .ToListAsync();

        return _mapper.Map<List<BookingDto>>(bookings);
    }

    public async Task<BookingDto?> GetBookingByIdAsync(int id)
    {
        var booking = await _context.Bookings
            .Include(b => b.Devotee)
            .Include(b => b.Seva)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return null;
        }

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<BookingDto?> CreateBookingAsync(CreateBookingDto createBookingDto)
    {
        var devoteeExists = await _context.Devotees
            .AnyAsync(d => d.Id == createBookingDto.DevoteeId);

        if (!devoteeExists)
        {
            return null;
        }

        var sevaExists = await _context.Sevas
            .AnyAsync(s => s.Id == createBookingDto.SevaId);

        if (!sevaExists)
        {
            return null;
        }

        var booking = _mapper.Map<Booking>(createBookingDto);

        booking.Status = BookingStatusHelper.Pending;
        booking.CreatedDate = DateTime.UtcNow;

        await _context.Bookings.AddAsync(booking);

        await _context.SaveChangesAsync();

        var createdBooking = await _context.Bookings
            .Include(b => b.Devotee)
            .Include(b => b.Seva)
            .FirstOrDefaultAsync(b => b.Id == booking.Id);

        return _mapper.Map<BookingDto>(createdBooking);
    }

    public async Task<BookingDto?> UpdateBookingStatusAsync(
        int id,
        UpdateBookingStatusDto updateBookingStatusDto)
    {
        var booking = await _context.Bookings
            .Include(b => b.Devotee)
            .Include(b => b.Seva)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return null;
        }

        if (!BookingStatusHelper.IsValidStatus(updateBookingStatusDto.Status))
        {
            return null;
        }

        booking.Status = BookingStatusHelper.NormalizeStatus(updateBookingStatusDto.Status);
        booking.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<bool> DeleteBookingAsync(int id)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return false;
        }

        _context.Bookings.Remove(booking);

        await _context.SaveChangesAsync();

        return true;
    }
}