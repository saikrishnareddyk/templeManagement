using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TempleManagementApi.Data;
using TempleManagementApi.DTOs;
using TempleManagementApi.Interfaces;
using TempleManagementApi.Models;

namespace TempleManagementApi.Services;

public class SevaService : ISevaService
{
    private readonly TempleDbContext _context;
    private readonly IMapper _mapper;

    public SevaService(TempleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SevaDto>> GetAllSevasAsync()
    {
        var sevas = await _context.Sevas
            .OrderByDescending(s => s.CreatedDate)
            .ToListAsync();

        return _mapper.Map<List<SevaDto>>(sevas);
    }

    public async Task<SevaDto?> GetSevaByIdAsync(int id)
    {
        var seva = await _context.Sevas
            .FirstOrDefaultAsync(s => s.Id == id);

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

        await _context.Sevas.AddAsync(seva);

        await _context.SaveChangesAsync();

        return _mapper.Map<SevaDto>(seva);
    }

    public async Task<SevaDto?> UpdateSevaAsync(int id, UpdateSevaDto updateSevaDto)
    {
        var seva = await _context.Sevas
            .FirstOrDefaultAsync(s => s.Id == id);

        if (seva == null)
        {
            return null;
        }

        _mapper.Map(updateSevaDto, seva);

        seva.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return _mapper.Map<SevaDto>(seva);
    }

    public async Task<bool> DeleteSevaAsync(int id)
    {
        var seva = await _context.Sevas
            .FirstOrDefaultAsync(s => s.Id == id);

        if (seva == null)
        {
            return false;
        }

        var hasBookings = await _context.Bookings
            .AnyAsync(b => b.SevaId == id);

        if (hasBookings)
        {
            return false;
        }

        _context.Sevas.Remove(seva);

        await _context.SaveChangesAsync();

        return true;
    }
}