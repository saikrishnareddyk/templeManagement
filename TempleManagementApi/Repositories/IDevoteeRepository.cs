using TempleManagementApi.Models;

namespace TempleManagementApi.Repositories;

public interface IDevoteeRepository
{
    Task<List<Devotee>> GetAllAsync();

    Task<Devotee?> GetByIdAsync(int id);

    Task AddAsync(Devotee devotee);

    void Delete(Devotee devotee);

    Task<bool> SaveChangesAsync();
}