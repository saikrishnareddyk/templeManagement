using AutoMapper;
using TempleManagementApi.DTOs;
using TempleManagementApi.Interfaces;
using TempleManagementApi.Models;
using TempleManagementApi.Repositories;

namespace TempleManagementApi.Services;

public class SevaService : ISevaService
{
    private readonly ISevaRepository _sevaRepository;
    private readonly IMapper _mapper;

    public SevaService(ISevaRepository sevaRepository, IMapper mapper)
    {
        _sevaRepository = sevaRepository;
        _mapper = mapper;
    }

    public async Task<List<SevaDto>> GetAllSevasAsync()
    {
        var sevas = await _sevaRepository.GetAllAsync();

        return _mapper.Map<List<SevaDto>>(sevas);
    }

    public async Task<SevaDto?> GetSevaByIdAsync(int id)
    {
        var seva = await _sevaRepository.GetByIdAsync(id);

        if (seva == null)
        {
            return null;
        }

        return _mapper.Map<SevaDto>(seva);
    }

    public async Task<SevaDto> CreateSevaAsync(CreateSevaDto createSevaDto)
    {
        var seva = _mapper.Map<Seva>(createSevaDto);

        seva.CreatedDate = DateTime.UtcNow;

        await _sevaRepository.AddAsync(seva);

        await _sevaRepository.SaveChangesAsync();

        return _mapper.Map<SevaDto>(seva);
    }

    public async Task<SevaDto?> UpdateSevaAsync(int id, UpdateSevaDto updateSevaDto)
    {
        var seva = await _sevaRepository.GetByIdAsync(id);

        if (seva == null)
        {
            return null;
        }

        _mapper.Map(updateSevaDto, seva);

        seva.UpdatedDate = DateTime.UtcNow;

        await _sevaRepository.SaveChangesAsync();

        return _mapper.Map<SevaDto>(seva);
    }

    public async Task<bool> DeleteSevaAsync(int id)
    {
        var seva = await _sevaRepository.GetByIdAsync(id);

        if (seva == null)
        {
            return false;
        }

        var hasBookings = await _sevaRepository.HasBookingsAsync(id);

        if (hasBookings)
        {
            return false;
        }

        _sevaRepository.Delete(seva);

        await _sevaRepository.SaveChangesAsync();

        return true;
    }
}