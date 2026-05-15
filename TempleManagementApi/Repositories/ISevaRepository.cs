using TempleManagementApi.Models;

namespace TempleManagementApi.Repositories;

public interface ISevaRepository
{
    Task<List<Seva>> GetAllAsync();

    Task<Seva?> GetByIdAsync(int id);

    Task AddAsync(Seva seva);

    void Delete(Seva seva);

    Task<bool> HasBookingsAsync(int sevaId);

    Task<bool> SaveChangesAsync();
}