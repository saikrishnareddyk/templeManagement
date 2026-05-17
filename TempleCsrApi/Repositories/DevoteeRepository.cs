using Microsoft.EntityFrameworkCore;
using TempleCsrApi.Data;
using TempleCsrApi.Models;

namespace TempleCsrApi.Repositories
{
    public class DevoteeRepository : IDevoteeRepository
    {
        private readonly TempleDbContext _context;

        public DevoteeRepository(TempleDbContext context)
        {
            _context = context;
        }

        public async Task<List<Devotee>> GetAllAsync()
        {
            return await _context.Devotees.ToListAsync();
        }

        public async Task<Devotee?> GetByIdAsync(int id)
        {
            return await _context.Devotees.FindAsync(id);
        }

        public async Task<Devotee?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Devotees
                .FirstOrDefaultAsync(d => d.PhoneNumber == phoneNumber);
        }

        public async Task<Devotee> AddAsync(Devotee devotee)
        {
            _context.Devotees.Add(devotee);
            await _context.SaveChangesAsync();

            return devotee;
        }

        public async Task UpdateAsync(Devotee devotee)
        {
            _context.Devotees.Update(devotee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Devotee devotee)
        {
            _context.Devotees.Remove(devotee);
            await _context.SaveChangesAsync();
        }
    }
}