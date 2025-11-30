using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Observer;

/// <summary>
///     Logging observer for audit trail
///     Concrete observer that maintains shipment history logs
/// </summary>
public class LoggingObserver : IShipmentObserver
{
    private readonly List<string> _auditLog = new();

    public string ObserverName => "Audit Logger";

    public void OnShipmentStatusChanged(string shipmentId, string oldStatus, string newStatus, string location)
    {
        var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Shipment {shipmentId}: {oldStatus} -> {newStatus} @ {location}";
        _auditLog.Add(logEntry);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"  [LOG] {logEntry}");
        Console.WriteLine($"  [LOG] Total audit entries: {_auditLog.Count}");
        Console.ResetColor();
    }

    public List<string> GetAuditLog() => new List<string>(_auditLog);
}
