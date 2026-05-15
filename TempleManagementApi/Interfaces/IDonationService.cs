using TempleManagementApi.DTOs;

namespace TempleManagementApi.Interfaces;

public interface IDonationService
{
    Task<List<DonationDto>> GetAllDonationsAsync();

    Task<DonationDto?> GetDonationByIdAsync(int id);

    Task<DonationDto?> CreateDonationAsync(CreateDonationDto createDonationDto);

    Task<DonationDto?> UpdateDonationAsync(int id, UpdateDonationDto updateDonationDto);

    Task<bool> DeleteDonationAsync(int id);
}