using TempleCsrApi.Models;

namespace TempleCsrApi.Services
{
    public interface IDevoteeService
    {
        Task<List<Devotee>> GetAllAsync();

        Task<Devotee?> GetByIdAsync(int id);

        Task<Devotee?> CreateAsync(Devotee devotee);

        Task<Devotee?> UpdateAsync(int id, Devotee devotee);

        Task<bool> DeleteAsync(int id);
    }
}