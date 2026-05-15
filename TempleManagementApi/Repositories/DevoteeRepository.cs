using Microsoft.EntityFrameworkCore;
using TempleManagementApi.Data;
using TempleManagementApi.Models;

namespace TempleManagementApi.Repositories;

public class DevoteeRepository : IDevoteeRepository
{
    private readonly TempleDbContext _context;

    public DevoteeRepository(TempleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Devotee>> GetAllAsync()
    {
        return await _context.Devotees
            .OrderByDescending(d => d.CreatedDate)
            .ToListAsync();
    }

    public async Task<Devotee?> GetByIdAsync(int id)
    {
        return await _context.Devotees
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task AddAsync(Devotee devotee)
    {
        await _context.Devotees.AddAsync(devotee);
    }

    public void Delete(Devotee devotee)
    {
        _context.Devotees.Remove(devotee);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}