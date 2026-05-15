using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TempleManagementApi.Data;
using TempleManagementApi.DTOs;
using TempleManagementApi.Interfaces;
using TempleManagementApi.Models;

namespace TempleManagementApi.Services;

public class DevoteeService : IDevoteeService
{
    private readonly TempleDbContext _context;
    private readonly IMapper _mapper;

    public DevoteeService(TempleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DevoteeDto>> GetAllDevoteesAsync()
    {
        var devotees = await _context.Devotees
            .OrderByDescending(d => d.CreatedDate)
            .ToListAsync();

        return _mapper.Map<List<DevoteeDto>>(devotees);
    }

    public async Task<DevoteeDto?> GetDevoteeByIdAsync(int id)
    {
        var devotee = await _context.Devotees
            .FirstOrDefaultAsync(d => d.Id == id);

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

        await _context.Devotees.AddAsync(devotee);

        await _context.SaveChangesAsync();

        return _mapper.Map<DevoteeDto>(devotee);
    }

    public async Task<DevoteeDto?> UpdateDevoteeAsync(int id, UpdateDevoteeDto updateDevoteeDto)
    {
        var devotee = await _context.Devotees
            .FirstOrDefaultAsync(d => d.Id == id);

        if (devotee == null)
        {
            return null;
        }

        _mapper.Map(updateDevoteeDto, devotee);

        devotee.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return _mapper.Map<DevoteeDto>(devotee);
    }

    public async Task<bool> DeleteDevoteeAsync(int id)
    {
        var devotee = await _context.Devotees
            .FirstOrDefaultAsync(d => d.Id == id);

        if (devotee == null)
        {
            return false;
        }

        _context.Devotees.Remove(devotee);

        await _context.SaveChangesAsync();

        return true;
    }
}