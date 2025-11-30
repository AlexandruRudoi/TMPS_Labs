namespace Lab_3.Services.Adapters;

/// <summary>
///     Legacy GPS tracking system (Adaptee)
///     Third-party system with incompatible interface
/// </summary>
public class LegacyGpsSystem
{
    /// <summary>
    ///     Gets GPS coordinates for a vehicle
    ///     Legacy method with different signature
    /// </summary>
    public string GetVehicleCoordinates(string vehicleId)
    {
        // Simulate GPS tracking
        var random = new Random();
        var latitude = 40.0 + random.NextDouble() * 5.0;
        var longitude = -74.0 + random.NextDouble() * 5.0;
        
        return $"GPS: {latitude:F6}, {longitude:F6}";
    }
    
    /// <summary>
    ///     Gets vehicle movement log
    /// </summary>
    public string[] GetVehicleLog(string vehicleId)
    {
        return new[]
        {
            $"[{DateTime.Now.AddHours(-3):HH:mm}] Departed from warehouse",
            $"[{DateTime.Now.AddHours(-2):HH:mm}] En route - Highway 101",
            $"[{DateTime.Now.AddHours(-1):HH:mm}] Approaching destination area",
            $"[{DateTime.Now:HH:mm}] Current location"
        };
    }
    
    /// <summary>
    ///     Calculates arrival time based on distance and speed
    /// </summary>
    public long GetEstimatedArrivalTimestamp(string vehicleId)
    {
        // Returns Unix timestamp
        return ((DateTimeOffset)DateTime.Now.AddHours(2)).ToUnixTimeSeconds();
    }
}
