using AutoMapper;
using TempleManagementApi.DTOs;
using TempleManagementApi.Helpers;
using TempleManagementApi.Interfaces;
using TempleManagementApi.Models;
using TempleManagementApi.Repositories;

namespace TempleManagementApi.Services;

public class DonationService : IDonationService
{
    private readonly IDonationRepository _donationRepository;
    private readonly IMapper _mapper;

    public DonationService(IDonationRepository donationRepository, IMapper mapper)
    {
        _donationRepository = donationRepository;
        _mapper = mapper;
    }

    public async Task<List<DonationDto>> GetAllDonationsAsync()
    {
        var donations = await _donationRepository.GetAllWithDevoteeAsync();

        return _mapper.Map<List<DonationDto>>(donations);
    }

    public async Task<DonationDto?> GetDonationByIdAsync(int id)
    {
        var donation = await _donationRepository.GetByIdWithDevoteeAsync(id);

        if (donation == null)
        {
            return null;
        }

        return _mapper.Map<DonationDto>(donation);
    }

    public async Task<DonationDto?> CreateDonationAsync(CreateDonationDto createDonationDto)
    {
        var devoteeExists = await _donationRepository.DevoteeExistsAsync(createDonationDto.DevoteeId);

        if (!devoteeExists)
        {
            return null;
        }

        if (!PaymentModeHelper.IsValidPaymentMode(createDonationDto.PaymentMode))
        {
            return null;
        }

        var donation = _mapper.Map<Donation>(createDonationDto);

        donation.PaymentMode = PaymentModeHelper.NormalizePaymentMode(createDonationDto.PaymentMode);
        donation.DonationDate = DateTime.UtcNow;
        donation.CreatedDate = DateTime.UtcNow;

        await _donationRepository.AddAsync(donation);

        await _donationRepository.SaveChangesAsync();

        var createdDonation = await _donationRepository.GetByIdWithDevoteeAsync(donation.Id);

        return _mapper.Map<DonationDto>(createdDonation);
    }

    public async Task<DonationDto?> UpdateDonationAsync(int id, UpdateDonationDto updateDonationDto)
    {
        var donation = await _donationRepository.GetByIdWithDevoteeAsync(id);

        if (donation == null)
        {
            return null;
        }

        if (!PaymentModeHelper.IsValidPaymentMode(updateDonationDto.PaymentMode))
        {
            return null;
        }

        _mapper.Map(updateDonationDto, donation);

        donation.PaymentMode = PaymentModeHelper.NormalizePaymentMode(updateDonationDto.PaymentMode);
        donation.UpdatedDate = DateTime.UtcNow;

        await _donationRepository.SaveChangesAsync();

        return _mapper.Map<DonationDto>(donation);
    }

    public async Task<bool> DeleteDonationAsync(int id)
    {
        var donation = await _donationRepository.GetByIdAsync(id);

        if (donation == null)
        {
            return false;
        }

        _donationRepository.Delete(donation);

        await _donationRepository.SaveChangesAsync();

        return true;
    }
}