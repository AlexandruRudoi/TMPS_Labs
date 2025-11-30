namespace Lab_3.Services.Adapters;

/// <summary>
///     RFID tracking API (Adaptee)
///     Third-party system with different interface
/// </summary>
public class RfidTrackingApi
{
    /// <summary>
    ///     Scans RFID tag and returns package location
    /// </summary>
    public Dictionary<string, object> ScanRfidTag(string tagId)
    {
        return new Dictionary<string, object>
        {
            { "tag_id", tagId },
            { "location", "Distribution Center B - Zone 3" },
            { "last_scan_time", DateTime.Now },
            { "status_code", 200 }
        };
    }
    
    /// <summary>
    ///     Gets all scan events for a tag
    /// </summary>
    public List<Dictionary<string, object>> GetScanHistory(string tagId)
    {
        return new List<Dictionary<string, object>>
        {
            new() { { "location", "Warehouse A - Loading Dock" }, { "time", DateTime.Now.AddHours(-4) } },
            new() { { "location", "Transit Hub - Sorting Area" }, { "time", DateTime.Now.AddHours(-2) } },
            new() { { "location", "Distribution Center B - Zone 3" }, { "time", DateTime.Now } }
        };
    }
    
    /// <summary>
    ///     Predicts delivery based on RFID scan patterns
    /// </summary>
    public Dictionary<string, object> PredictDeliveryTime(string tagId)
    {
        return new Dictionary<string, object>
        {
            { "predicted_delivery", DateTime.Now.AddHours(3) },
            { "confidence", 0.87 }
        };
    }
}
