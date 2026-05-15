using AutoMapper;
using TempleManagementApi.DTOs;
using TempleManagementApi.Helpers;
using TempleManagementApi.Interfaces;
using TempleManagementApi.Models;
using TempleManagementApi.Repositories;

namespace TempleManagementApi.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public BookingService(IBookingRepository bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public async Task<List<BookingDto>> GetAllBookingsAsync()
    {
        var bookings = await _bookingRepository.GetAllWithDetailsAsync();

        return _mapper.Map<List<BookingDto>>(bookings);
    }

    public async Task<BookingDto?> GetBookingByIdAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(id);

        if (booking == null)
        {
            return null;
        }

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<BookingDto?> CreateBookingAsync(CreateBookingDto createBookingDto)
    {
        var devoteeExists = await _bookingRepository.DevoteeExistsAsync(createBookingDto.DevoteeId);

        if (!devoteeExists)
        {
            return null;
        }

        var sevaExists = await _bookingRepository.SevaExistsAsync(createBookingDto.SevaId);

        if (!sevaExists)
        {
            return null;
        }

        var booking = _mapper.Map<Booking>(createBookingDto);

        booking.Status = BookingStatusHelper.Pending;
        booking.CreatedDate = DateTime.UtcNow;

        await _bookingRepository.AddAsync(booking);

        await _bookingRepository.SaveChangesAsync();

        var createdBooking = await _bookingRepository.GetByIdWithDetailsAsync(booking.Id);

        return _mapper.Map<BookingDto>(createdBooking);
    }

    public async Task<BookingDto?> UpdateBookingStatusAsync(
        int id,
        UpdateBookingStatusDto updateBookingStatusDto)
    {
        var booking = await _bookingRepository.GetByIdWithDetailsAsync(id);

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

        await _bookingRepository.SaveChangesAsync();

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<bool> DeleteBookingAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);

        if (booking == null)
        {
            return false;
        }

        _bookingRepository.Delete(booking);

        await _bookingRepository.SaveChangesAsync();

        return true;
    }
}