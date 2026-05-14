using System.ComponentModel.DataAnnotations;

namespace TempleManagementApi.DTOs;

public class CreateBookingDto
{
    [Required(ErrorMessage = "DevoteeId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "DevoteeId must be valid")]
    public int DevoteeId { get; set; }

    [Required(ErrorMessage = "SevaId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "SevaId must be valid")]
    public int SevaId { get; set; }

    [Required(ErrorMessage = "Booking date is required")]
    public DateTime BookingDate { get; set; }
}