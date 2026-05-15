using TempleManagementApi.Models;

namespace TempleManagementApi.Repositories;

public interface IDonationRepository
{
    Task<List<Donation>> GetAllWithDevoteeAsync();

    Task<Donation?> GetByIdWithDevoteeAsync(int id);

    Task<Donation?> GetByIdAsync(int id);

    Task<bool> DevoteeExistsAsync(int devoteeId);

    Task AddAsync(Donation donation);

    void Delete(Donation donation);

    Task<bool> SaveChangesAsync();
}