namespace TempleManagementApi.DTOs;

public class DonationDto
{
    public int Id { get; set; }

    public int DevoteeId { get; set; }

    public string DevoteeName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string PaymentMode { get; set; } = string.Empty;

    public DateTime DonationDate { get; set; }

    public DateTime CreatedDate { get; set; }
}