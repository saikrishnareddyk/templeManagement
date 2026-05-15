using TempleManagementApi.Models;

namespace TempleManagementApi.Repositories;

public interface IBookingRepository
{
    Task<List<Booking>> GetAllWithDetailsAsync();

    Task<Booking?> GetByIdWithDetailsAsync(int id);

    Task<Booking?> GetByIdAsync(int id);

    Task<bool> DevoteeExistsAsync(int devoteeId);

    Task<bool> SevaExistsAsync(int sevaId);

    Task AddAsync(Booking booking);

    void Delete(Booking booking);

    Task<bool> SaveChangesAsync();
}