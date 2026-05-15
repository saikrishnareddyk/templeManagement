using Microsoft.EntityFrameworkCore;
using TempleManagementApi.Data;
using TempleManagementApi.Models;

namespace TempleManagementApi.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly TempleDbContext _context;

    public DonationRepository(TempleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Donation>> GetAllWithDevoteeAsync()
    {
        return await _context.Donations
            .Include(d => d.Devotee)
            .OrderByDescending(d => d.CreatedDate)
            .ToListAsync();
    }

    public async Task<Donation?> GetByIdWithDevoteeAsync(int id)
    {
        return await _context.Donations
            .Include(d => d.Devotee)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Donation?> GetByIdAsync(int id)
    {
        return await _context.Donations
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<bool> DevoteeExistsAsync(int devoteeId)
    {
        return await _context.Devotees
            .AnyAsync(d => d.Id == devoteeId);
    }

    public async Task AddAsync(Donation donation)
    {
        await _context.Donations.AddAsync(donation);
    }

    public void Delete(Donation donation)
    {
        _context.Donations.Remove(donation);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}