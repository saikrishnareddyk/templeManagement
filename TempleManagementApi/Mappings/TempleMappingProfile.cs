using AutoMapper;
using TempleManagementApi.DTOs;
using TempleManagementApi.Models;

namespace TempleManagementApi.Mappings;

public class TempleMappingProfile : Profile
{
    public TempleMappingProfile()
    {
        // -----------------------------
        // Devotee mappings
        // -----------------------------

        CreateMap<Devotee, DevoteeDto>();

        CreateMap<CreateDevoteeDto, Devotee>();

        CreateMap<UpdateDevoteeDto, Devotee>();


        // -----------------------------
        // Seva mappings
        // -----------------------------

        CreateMap<Seva, SevaDto>();

        CreateMap<CreateSevaDto, Seva>();

        CreateMap<UpdateSevaDto, Seva>();


        // -----------------------------
        // Booking mappings
        // -----------------------------

        CreateMap<Booking, BookingDto>()
            .ForMember(
                destination => destination.DevoteeName,
                option => option.MapFrom(source =>
                    source.Devotee != null ? source.Devotee.FullName : string.Empty
                )
            )
            .ForMember(
                destination => destination.SevaName,
                option => option.MapFrom(source =>
                    source.Seva != null ? source.Seva.SevaName : string.Empty
                )
            );

        CreateMap<CreateBookingDto, Booking>();

        CreateMap<UpdateBookingStatusDto, Booking>();


        // -----------------------------
        // Donation mappings
        // -----------------------------

        CreateMap<Donation, DonationDto>()
            .ForMember(
                destination => destination.DevoteeName,
                option => option.MapFrom(source =>
                    source.Devotee != null ? source.Devotee.FullName : string.Empty
                )
            );

        CreateMap<CreateDonationDto, Donation>();

        CreateMap<UpdateDonationDto, Donation>();
    }
}