using TempleCsrApi.Models;

namespace TempleCsrApi.Repositories
{
    public interface IDevoteeRepository
    {
        Task<List<Devotee>> GetAllAsync();

        Task<Devotee?> GetByIdAsync(int id);

        Task<Devotee?> GetByPhoneNumberAsync(string phoneNumber);

        Task<Devotee> AddAsync(Devotee devotee);

        Task UpdateAsync(Devotee devotee);

        Task DeleteAsync(Devotee devotee);
    }
}