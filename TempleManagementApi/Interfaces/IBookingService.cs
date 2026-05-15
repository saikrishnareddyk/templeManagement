using TempleManagementApi.DTOs;

namespace TempleManagementApi.Interfaces;

public interface IBookingService
{
    Task<List<BookingDto>> GetAllBookingsAsync();

    Task<BookingDto?> GetBookingByIdAsync(int id);

    Task<BookingDto?> CreateBookingAsync(CreateBookingDto createBookingDto);

    Task<BookingDto?> UpdateBookingStatusAsync(int id, UpdateBookingStatusDto updateBookingStatusDto);

    Task<bool> DeleteBookingAsync(int id);
}