using TempleCsrApi.Models;
using TempleCsrApi.Repositories;

namespace TempleCsrApi.Services
{
    public class DevoteeService : IDevoteeService
    {
        private readonly IDevoteeRepository _devoteeRepository;

        public DevoteeService(IDevoteeRepository devoteeRepository)
        {
            _devoteeRepository = devoteeRepository;
        }

        public async Task<List<Devotee>> GetAllAsync()
        {
            return await _devoteeRepository.GetAllAsync();
        }

        public async Task<Devotee?> GetByIdAsync(int id)
        {
            return await _devoteeRepository.GetByIdAsync(id);
        }

        public async Task<Devotee?> CreateAsync(Devotee devotee)
        {
            var existingDevotee = await _devoteeRepository
                .GetByPhoneNumberAsync(devotee.PhoneNumber);

            if (existingDevotee != null)
            {
                return null;
            }

            devotee.CreatedAt = DateTime.UtcNow;

            return await _devoteeRepository.AddAsync(devotee);
        }

        public async Task<Devotee?> UpdateAsync(int id, Devotee devotee)
        {
            var existingDevotee = await _devoteeRepository.GetByIdAsync(id);

            if (existingDevotee == null)
            {
                return null;
            }

            existingDevotee.FullName = devotee.FullName;
            existingDevotee.PhoneNumber = devotee.PhoneNumber;
            existingDevotee.Email = devotee.Email;
            existingDevotee.Gothram = devotee.Gothram;
            existingDevotee.UpdatedAt = DateTime.UtcNow;

            await _devoteeRepository.UpdateAsync(existingDevotee);

            return existingDevotee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingDevotee = await _devoteeRepository.GetByIdAsync(id);

            if (existingDevotee == null)
            {
                return false;
            }

            await _devoteeRepository.DeleteAsync(existingDevotee);

            return true;
        }
    }
}