using System.ComponentModel.DataAnnotations;

namespace TempleManagementApi.DTOs;

public class UpdateBookingStatusDto
{
    [Required(ErrorMessage = "Status is required")]
    [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
    public string Status { get; set; } = string.Empty;
}