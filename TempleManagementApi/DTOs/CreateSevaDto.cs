using System.ComponentModel.DataAnnotations;

namespace TempleManagementApi.DTOs;

public class CreateSevaDto
{
    [Required(ErrorMessage = "Seva name is required")]
    [StringLength(100, ErrorMessage = "Seva name cannot exceed 100 characters")]
    public string SevaName { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required")]
    [Range(1, 100000, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Duration is required")]
    [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes")]
    public int DurationMinutes { get; set; }
}