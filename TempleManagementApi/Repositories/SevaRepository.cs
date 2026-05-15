using Microsoft.EntityFrameworkCore;
using TempleManagementApi.Data;
using TempleManagementApi.Models;

namespace TempleManagementApi.Repositories;

public class SevaRepository : ISevaRepository
{
    private readonly TempleDbContext _context;

    public SevaRepository(TempleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Seva>> GetAllAsync()
    {
        return await _context.Sevas
            .OrderByDescending(s => s.CreatedDate)
            .ToListAsync();
    }

    public async Task<Seva?> GetByIdAsync(int id)
    {
        return await _context.Sevas
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(Seva seva)
    {
        await _context.Sevas.AddAsync(seva);
    }

    public void Delete(Seva seva)
    {
        _context.Sevas.Remove(seva);
    }

    public async Task<bool> HasBookingsAsync(int sevaId)
    {
        return await _context.Bookings
            .AnyAsync(b => b.SevaId == sevaId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}