using Microsoft.EntityFrameworkCore;
using TempleManagementApi.Data;
using TempleManagementApi.Models;

namespace TempleManagementApi.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly TempleDbContext _context;

    public BookingRepository(TempleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Booking>> GetAllWithDetailsAsync()
    {
        return await _context.Bookings
            .Include(b => b.Devotee)
            .Include(b => b.Seva)
            .OrderByDescending(b => b.CreatedDate)
            .ToListAsync();
    }

    public async Task<Booking?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Bookings
            .Include(b => b.Devotee)
            .Include(b => b.Seva)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<bool> DevoteeExistsAsync(int devoteeId)
    {
        return await _context.Devotees
            .AnyAsync(d => d.Id == devoteeId);
    }

    public async Task<bool> SevaExistsAsync(int sevaId)
    {
        return await _context.Sevas
            .AnyAsync(s => s.Id == sevaId);
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    public void Delete(Booking booking)
    {
        _context.Bookings.Remove(booking);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}