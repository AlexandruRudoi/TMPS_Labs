using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Observer;

/// <summary>
///     Subject class that maintains list of observers and notifies them of changes
///     Part of the Observer behavioral pattern
/// </summary>
public class ShipmentTracker
{
    private readonly List<IShipmentObserver> _observers = new();
    private readonly Dictionary<string, string> _shipmentStatuses = new();
    private readonly Dictionary<string, string> _shipmentLocations = new();

    /// <summary>
    ///     Attach an observer to receive notifications
    /// </summary>
    public void Attach(IShipmentObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
            Console.WriteLine($"[Tracker] Attached observer: {observer.ObserverName}");
        }
    }

    /// <summary>
    ///     Detach an observer from receiving notifications
    /// </summary>
    public void Detach(IShipmentObserver observer)
    {
        if (_observers.Remove(observer))
        {
            Console.WriteLine($"[Tracker] Detached observer: {observer.ObserverName}");
        }
    }

    /// <summary>
    ///     Update shipment status and notify all observers
    /// </summary>
    public void UpdateShipmentStatus(string shipmentId, string newStatus, string location)
    {
        var oldStatus = _shipmentStatuses.ContainsKey(shipmentId) 
            ? _shipmentStatuses[shipmentId] 
            : "Unknown";

        _shipmentStatuses[shipmentId] = newStatus;
        _shipmentLocations[shipmentId] = location;

        Console.WriteLine($"\n[Tracker] Shipment {shipmentId} status changed: {oldStatus} -> {newStatus}");
        Console.WriteLine($"[Tracker] Location: {location}");
        Console.WriteLine($"[Tracker] Notifying {_observers.Count} observer(s)...\n");

        NotifyObservers(shipmentId, oldStatus, newStatus, location);
    }

    /// <summary>
    ///     Notify all attached observers about the status change
    /// </summary>
    private void NotifyObservers(string shipmentId, string oldStatus, string newStatus, string location)
    {
        foreach (var observer in _observers)
        {
            observer.OnShipmentStatusChanged(shipmentId, oldStatus, newStatus, location);
        }
    }

    /// <summary>
    ///     Get current number of observers
    /// </summary>
    public int GetObserverCount() => _observers.Count;
}
