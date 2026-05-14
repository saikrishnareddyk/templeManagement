namespace TempleManagementApi.DTOs;

public class BookingDto
{
    public int Id { get; set; }

    public int DevoteeId { get; set; }

    public string DevoteeName { get; set; } = string.Empty;

    public int SevaId { get; set; }

    public string SevaName { get; set; } = string.Empty;

    public DateTime BookingDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
}