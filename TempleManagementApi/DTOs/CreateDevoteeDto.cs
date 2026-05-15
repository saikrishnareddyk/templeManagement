using System.ComponentModel.DataAnnotations;

namespace TempleManagementApi.DTOs;

public class CreateDevoteeDto
{
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 100 characters")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mobile number is required")]
    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Mobile number must be a valid 10 digit Indian mobile number")]
    public string MobileNumber { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Invalid email address")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    public string Email { get; set; } = string.Empty;

    [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
    public string Address { get; set; } = string.Empty;
}