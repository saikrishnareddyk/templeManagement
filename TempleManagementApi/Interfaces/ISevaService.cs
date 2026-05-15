using TempleManagementApi.DTOs;

namespace TempleManagementApi.Interfaces;

public interface ISevaService
{
    Task<List<SevaDto>> GetAllSevasAsync();

    Task<SevaDto?> GetSevaByIdAsync(int id);

    Task<SevaDto> CreateSevaAsync(CreateSevaDto createSevaDto);

    Task<SevaDto?> UpdateSevaAsync(int id, UpdateSevaDto updateSevaDto);

    Task<bool> DeleteSevaAsync(int id);
}