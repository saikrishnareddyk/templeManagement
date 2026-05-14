using System.ComponentModel.DataAnnotations;

namespace TempleManagementApi.DTOs;

public class UpdateDonationDto
{
    [Required(ErrorMessage = "Amount is required")]
    [Range(1, 10000000, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Payment mode is required")]
    [StringLength(50, ErrorMessage = "Payment mode cannot exceed 50 characters")]
    public string PaymentMode { get; set; } = string.Empty;
}