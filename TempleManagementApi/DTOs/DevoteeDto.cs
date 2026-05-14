namespace TempleManagementApi.DTOs;

public class DevoteeDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string MobileNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
}