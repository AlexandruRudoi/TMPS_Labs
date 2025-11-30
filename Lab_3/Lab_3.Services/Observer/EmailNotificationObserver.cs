using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Observer;

/// <summary>
///     Email notification observer
///     Concrete observer that sends email notifications
/// </summary>
public class EmailNotificationObserver : IShipmentObserver
{
    private readonly string _emailAddress;

    public EmailNotificationObserver(string emailAddress)
    {
        _emailAddress = emailAddress;
    }

    public string ObserverName => $"Email Notifier ({_emailAddress})";

    public void OnShipmentStatusChanged(string shipmentId, string oldStatus, string newStatus, string location)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  [EMAIL] To: {_emailAddress}");
        Console.WriteLine($"  [EMAIL] Subject: Shipment {shipmentId} Update");
        Console.WriteLine($"  [EMAIL] Body: Your shipment status changed from '{oldStatus}' to '{newStatus}'");
        Console.WriteLine($"  [EMAIL] Current location: {location}");
        Console.ResetColor();
    }
}
