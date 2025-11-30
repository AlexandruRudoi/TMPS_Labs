namespace Lab_3.Domain.Interfaces;

/// <summary>
///     Unified interface for all tracking systems
///     Target interface for Adapter pattern
/// </summary>
public interface ITrackingSystem
{
    /// <summary>
    ///     Gets the tracking system name
    /// </summary>
    string SystemName { get; }

    /// <summary>
    ///     Tracks a shipment or package
    /// </summary>
    /// <param name="identifier">Unique tracking identifier</param>
    /// <returns>Current location information</returns>
    string Track(string identifier);

    /// <summary>
    ///     Gets detailed tracking history
    /// </summary>
    /// <param name="identifier">Unique tracking identifier</param>
    /// <returns>List of tracking events</returns>
    List<string> GetTrackingHistory(string identifier);

    /// <summary>
    ///     Gets estimated delivery time
    /// </summary>
    /// <param name="identifier">Unique tracking identifier</param>
    /// <returns>Estimated delivery date and time</returns>
    DateTime GetEstimatedDelivery(string identifier);
}