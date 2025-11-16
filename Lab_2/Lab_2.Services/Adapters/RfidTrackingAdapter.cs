using Lab_2.Domain.Interfaces;

namespace Lab_2.Services.Adapters;

/// <summary>
///     Adapter for RFID Tracking API
///     Converts RFID dictionary-based API to unified ITrackingSystem interface
/// </summary>
public class RfidTrackingAdapter : ITrackingSystem
{
    private readonly RfidTrackingApi _rfidApi;

    /// <summary>
    ///     Initializes a new instance of the RfidTrackingAdapter
    /// </summary>
    public RfidTrackingAdapter()
    {
        _rfidApi = new RfidTrackingApi();
    }
    
    /// <inheritdoc />
    public string SystemName => "RFID Tracking System";

    /// <inheritdoc />
    public string Track(string identifier)
    {
        // Adapt: Extract location from dictionary response
        var scanData = _rfidApi.ScanRfidTag(identifier);
        var location = scanData["location"];
        var scanTime = scanData["last_scan_time"];
        
        return $"{SystemName}: Package at {location} (Last scanned: {scanTime:HH:mm})";
    }

    /// <inheritdoc />
    public List<string> GetTrackingHistory(string identifier)
    {
        // Adapt: Convert dictionary list to string list with formatting
        var scanHistory = _rfidApi.GetScanHistory(identifier);
        
        return scanHistory.Select(scan => 
            $"{SystemName} - [{((DateTime)scan["time"]):HH:mm}] {scan["location"]}"
        ).ToList();
    }

    /// <inheritdoc />
    public DateTime GetEstimatedDelivery(string identifier)
    {
        // Adapt: Extract DateTime from dictionary response
        var prediction = _rfidApi.PredictDeliveryTime(identifier);
        return (DateTime)prediction["predicted_delivery"];
    }
}
