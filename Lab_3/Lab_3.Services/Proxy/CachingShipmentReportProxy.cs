using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Proxy;

/// <summary>
///     Caching Proxy for performance optimization
///     Caches report data to avoid repeated expensive operations
/// </summary>
public class CachingShipmentReportProxy : IShipmentReport
{
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);
    private readonly string _shipmentId;
    private string _cachedPdf;
    private DateTime _cacheTime;
    private ShipmentReport _realReport;

    public CachingShipmentReportProxy(string shipmentId)
    {
        _shipmentId = shipmentId;
        Console.WriteLine($"   Caching proxy created for shipment {_shipmentId}");
    }

    public void Load()
    {
        if (_realReport == null)
        {
            Console.WriteLine("   Cache miss - loading report...");
            _realReport = new ShipmentReport(_shipmentId);
            _realReport.Load();
        }
        else
        {
            Console.WriteLine("    Cache hit - report already loaded");
        }
    }

    public void Display()
    {
        if (_realReport == null)
        {
            Console.WriteLine("    Cache miss - creating and loading report...");
            _realReport = new ShipmentReport(_shipmentId);
        }
        else
        {
            Console.WriteLine("    Using cached report");
        }

        _realReport.Display();
    }

    public string ExportToPdf()
    {
        // Check if cached PDF is still valid
        if (_cachedPdf != null && DateTime.Now - _cacheTime < _cacheExpiration)
        {
            Console.WriteLine(
                $"    Cache hit - returning cached PDF (age: {(DateTime.Now - _cacheTime).TotalSeconds:F0}s)");
            Console.WriteLine($"   Cached PDF: {_cachedPdf}");
            return _cachedPdf;
        }

        Console.WriteLine("    Cache miss or expired - generating new PDF...");

        if (_realReport == null)
            _realReport = new ShipmentReport(_shipmentId);

        _cachedPdf = _realReport.ExportToPdf();
        _cacheTime = DateTime.Now;

        Console.WriteLine($"   PDF cached for {_cacheExpiration.TotalMinutes} minutes");

        return _cachedPdf;
    }

    public string GetSummary()
    {
        return $"Shipment Report for {_shipmentId} (Cached)";
    }
}