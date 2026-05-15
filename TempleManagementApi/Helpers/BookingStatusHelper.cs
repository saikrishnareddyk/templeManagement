namespace TempleManagementApi.Helpers;

public static class BookingStatusHelper
{
    public const string Pending = "Pending";
    public const string Confirmed = "Confirmed";
    public const string Completed = "Completed";
    public const string Cancelled = "Cancelled";

    public static readonly string[] AllowedStatuses =
    {
        Pending,
        Confirmed,
        Completed,
        Cancelled
    };

    public static bool IsValidStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            return false;
        }

        return AllowedStatuses.Any(s =>
            s.Equals(status, StringComparison.OrdinalIgnoreCase));
    }

    public static string NormalizeStatus(string status)
    {
        return AllowedStatuses.First(s =>
            s.Equals(status, StringComparison.OrdinalIgnoreCase));
    }
}