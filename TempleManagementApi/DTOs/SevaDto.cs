namespace TempleManagementApi.DTOs;

public class SevaDto
{
    public int Id { get; set; }

    public string SevaName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int DurationMinutes { get; set; }

    public DateTime CreatedDate { get; set; }
}