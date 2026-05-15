namespace TempleManagementApi.Helpers;

public static class PaymentModeHelper
{
    public const string Cash = "Cash";
    public const string Upi = "UPI";
    public const string Card = "Card";
    public const string BankTransfer = "Bank Transfer";

    public static readonly string[] AllowedPaymentModes =
    {
        Cash,
        Upi,
        Card,
        BankTransfer
    };

    public static bool IsValidPaymentMode(string paymentMode)
    {
        if (string.IsNullOrWhiteSpace(paymentMode))
        {
            return false;
        }

        return AllowedPaymentModes.Any(mode =>
            mode.Equals(paymentMode, StringComparison.OrdinalIgnoreCase));
    }

    public static string NormalizePaymentMode(string paymentMode)
    {
        return AllowedPaymentModes.First(mode =>
            mode.Equals(paymentMode, StringComparison.OrdinalIgnoreCase));
    }
}