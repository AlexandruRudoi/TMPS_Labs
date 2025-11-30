using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Observer;

/// <summary>
///     Dashboard notification observer
///     Concrete observer that updates a monitoring dashboard
/// </summary>
public class DashboardObserver : IShipmentObserver
{
    private readonly string _dashboardId;
    private int _updateCount = 0;

    public DashboardObserver(string dashboardId)
    {
        _dashboardId = dashboardId;
    }

    public string ObserverName => $"Dashboard Monitor ({_dashboardId})";

    public void OnShipmentStatusChanged(string shipmentId, string oldStatus, string newStatus, string location)
    {
        _updateCount++;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  [DASHBOARD-{_dashboardId}] Shipment {shipmentId} updated");
        Console.WriteLine($"  [DASHBOARD-{_dashboardId}] Status: {newStatus} | Location: {location}");
        Console.WriteLine($"  [DASHBOARD-{_dashboardId}] Total updates: {_updateCount}");
        Console.ResetColor();
    }
}
