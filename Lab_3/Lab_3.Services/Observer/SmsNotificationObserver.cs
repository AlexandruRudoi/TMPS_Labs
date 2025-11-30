using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Observer;

/// <summary>
///     SMS notification observer
///     Concrete observer that sends SMS notifications
/// </summary>
public class SmsNotificationObserver : IShipmentObserver
{
    private readonly string _phoneNumber;

    public SmsNotificationObserver(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
    }

    public string ObserverName => $"SMS Notifier ({_phoneNumber})";

    public void OnShipmentStatusChanged(string shipmentId, string oldStatus, string newStatus, string location)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  [SMS] To: {_phoneNumber}");
        Console.WriteLine($"  [SMS] Message: Shipment {shipmentId} is now '{newStatus}' at {location}");
        Console.ResetColor();
    }
}
