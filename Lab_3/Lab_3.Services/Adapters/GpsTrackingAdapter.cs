using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Adapters;

/// <summary>
///     Adapter for Legacy GPS System
///     Converts incompatible GPS interface to unified ITrackingSystem interface
/// </summary>
public class GpsTrackingAdapter : ITrackingSystem
{
    private readonly LegacyGpsSystem _gpsSystem;

    /// <summary>
    ///     Initializes a new instance of the GpsTrackingAdapter
    /// </summary>
    public GpsTrackingAdapter()
    {
        _gpsSystem = new LegacyGpsSystem();
    }
    
    /// <inheritdoc />
    public string SystemName => "GPS Tracking System";

    /// <inheritdoc />
    public string Track(string identifier)
    {
        // Adapt: Convert our identifier to GPS vehicle ID and call legacy method
        var coordinates = _gpsSystem.GetVehicleCoordinates(identifier);
        return $"{SystemName}: Vehicle at {coordinates}";
    }

    /// <inheritdoc />
    public List<string> GetTrackingHistory(string identifier)
    {
        // Adapt: Convert string array to List<string> and format
        var log = _gpsSystem.GetVehicleLog(identifier);
        return log.Select(entry => $"{SystemName} - {entry}").ToList();
    }

    /// <inheritdoc />
    public DateTime GetEstimatedDelivery(string identifier)
    {
        // Adapt: Convert Unix timestamp to DateTime
        var timestamp = _gpsSystem.GetEstimatedArrivalTimestamp(identifier);
        return DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
    }
}
