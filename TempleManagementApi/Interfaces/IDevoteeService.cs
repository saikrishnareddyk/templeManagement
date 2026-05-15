using TempleManagementApi.DTOs;

namespace TempleManagementApi.Interfaces;

public interface IDevoteeService
{
    Task<List<DevoteeDto>> GetAllDevoteesAsync();

    Task<DevoteeDto?> GetDevoteeByIdAsync(int id);

    Task<DevoteeDto> CreateDevoteeAsync(CreateDevoteeDto createDevoteeDto);

    Task<DevoteeDto?> UpdateDevoteeAsync(int id, UpdateDevoteeDto updateDevoteeDto);

    Task<bool> DeleteDevoteeAsync(int id);
}