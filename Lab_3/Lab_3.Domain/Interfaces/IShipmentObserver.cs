namespace Lab_3.Domain.Interfaces;

/// <summary>
///     Observer interface for shipment status notifications
///     Part of the Observer behavioral pattern
/// </summary>
public interface IShipmentObserver
{
    /// <summary>
    ///     Called when a shipment status changes
    /// </summary>
    /// <param name="shipmentId">The ID of the shipment</param>
    /// <param name="oldStatus">Previous status</param>
    /// <param name="newStatus">New status</param>
    /// <param name="location">Current location</param>
    void OnShipmentStatusChanged(string shipmentId, string oldStatus, string newStatus, string location);

    /// <summary>
    ///     Gets the observer name
    /// </summary>
    string ObserverName { get; }
}
