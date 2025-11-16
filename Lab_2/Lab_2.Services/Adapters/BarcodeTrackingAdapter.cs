using Lab_2.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace Lab_2.Services.Adapters;

/// <summary>
///     Adapter for Barcode Scanner System
///     Converts XML-based barcode interface to unified ITrackingSystem interface
/// </summary>
public class BarcodeTrackingAdapter : ITrackingSystem
{
    private readonly BarcodeScanner _scanner;

    /// <summary>
    ///     Initializes a new instance of the BarcodeTrackingAdapter
    /// </summary>
    public BarcodeTrackingAdapter()
    {
        _scanner = new BarcodeScanner();
    }
    
    /// <inheritdoc />
    public string SystemName => "Barcode Tracking System";

    /// <inheritdoc />
    public string Track(string identifier)
    {
        // Adapt: Parse XML response from barcode scanner
        var xmlData = _scanner.ScanBarcode(identifier);
        var facility = ExtractXmlValue(xmlData, "facility");
        var timestamp = ExtractXmlValue(xmlData, "timestamp");
        
        return $"{SystemName}: Package at {facility} (Scanned: {timestamp})";
    }

    /// <inheritdoc />
    public List<string> GetTrackingHistory(string identifier)
    {
        // Adapt: Parse XML history and convert to string list
        var xmlHistory = _scanner.GetBarcodeHistory(identifier);
        
        var facilities = Regex.Matches(xmlHistory, @"<facility>(.*?)</facility>");
        var times = Regex.Matches(xmlHistory, @"<time>(.*?)</time>");
        
        var history = new List<string>();
        for (int i = 0; i < facilities.Count && i < times.Count; i++)
        {
            history.Add($"{SystemName} - [{times[i].Groups[1].Value}] {facilities[i].Groups[1].Value}");
        }
        
        return history;
    }

    /// <inheritdoc />
    public DateTime GetEstimatedDelivery(string identifier)
    {
        // Adapt: Convert hours to DateTime
        var hours = _scanner.EstimateDeliveryHours(identifier);
        return DateTime.Now.AddHours(hours);
    }
    
    /// <summary>
    ///     Extracts value from simple XML tag
    /// </summary>
    private string ExtractXmlValue(string xml, string tag)
    {
        var match = Regex.Match(xml, $@"<{tag}>(.*?)</{tag}>");
        return match.Success ? match.Groups[1].Value : "Unknown";
    }
}
