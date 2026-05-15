using AutoMapper;
using TempleManagementApi.DTOs;
using TempleManagementApi.Interfaces;
using TempleManagementApi.Models;
using TempleManagementApi.Repositories;

namespace TempleManagementApi.Services;

public class DevoteeService : IDevoteeService
{
    private readonly IDevoteeRepository _devoteeRepository;
    private readonly IMapper _mapper;

    public DevoteeService(IDevoteeRepository devoteeRepository, IMapper mapper)
    {
        _devoteeRepository = devoteeRepository;
        _mapper = mapper;
    }

    public async Task<List<DevoteeDto>> GetAllDevoteesAsync()
    {
        var devotees = await _devoteeRepository.GetAllAsync();

        return _mapper.Map<List<DevoteeDto>>(devotees);
    }

    public async Task<DevoteeDto?> GetDevoteeByIdAsync(int id)
    {
        var devotee = await _devoteeRepository.GetByIdAsync(id);

        if (devotee == null)
        {
            return null;
        }

        return _mapper.Map<DevoteeDto>(devotee);
    }

    public async Task<DevoteeDto> CreateDevoteeAsync(CreateDevoteeDto createDevoteeDto)
    {
        var devotee = _mapper.Map<Devotee>(createDevoteeDto);

        devotee.CreatedDate = DateTime.UtcNow;

        await _devoteeRepository.AddAsync(devotee);

        await _devoteeRepository.SaveChangesAsync();

        return _mapper.Map<DevoteeDto>(devotee);
    }

    public async Task<DevoteeDto?> UpdateDevoteeAsync(int id, UpdateDevoteeDto updateDevoteeDto)
    {
        var devotee = await _devoteeRepository.GetByIdAsync(id);

        if (devotee == null)
        {
            return null;
        }

        _mapper.Map(updateDevoteeDto, devotee);

        devotee.UpdatedDate = DateTime.UtcNow;

        await _devoteeRepository.SaveChangesAsync();

        return _mapper.Map<DevoteeDto>(devotee);
    }

    public async Task<bool> DeleteDevoteeAsync(int id)
    {
        var devotee = await _devoteeRepository.GetByIdAsync(id);

        if (devotee == null)
        {
            return false;
        }

        _devoteeRepository.Delete(devotee);

        await _devoteeRepository.SaveChangesAsync();

        return true;
    }
}