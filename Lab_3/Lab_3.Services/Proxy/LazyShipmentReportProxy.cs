using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Proxy;

/// <summary>
///     Virtual Proxy for lazy loading
///     Delays expensive report loading until actually needed
/// </summary>
public class LazyShipmentReportProxy : IShipmentReport
{
    private readonly string _shipmentId;
    private ShipmentReport _realReport;

    public LazyShipmentReportProxy(string shipmentId)
    {
        _shipmentId = shipmentId;
        Console.WriteLine($"   Proxy created for shipment {_shipmentId} (report not loaded yet)");
    }

    public void Load()
    {
        if (_realReport == null)
        {
            Console.WriteLine($"   Proxy: Initializing real report for {_shipmentId}...");
            _realReport = new ShipmentReport(_shipmentId);
        }
        _realReport.Load();
    }

    public void Display()
    {
        if (_realReport == null)
        {
            Console.WriteLine($"   Proxy: First access - creating real report...");
            _realReport = new ShipmentReport(_shipmentId);
        }
        _realReport.Display();
    }

    public string ExportToPdf()
    {
        if (_realReport == null)
        {
            Console.WriteLine($"   Proxy: Creating real report for PDF export...");
            _realReport = new ShipmentReport(_shipmentId);
        }
        return _realReport.ExportToPdf();
    }

    public string GetSummary()
    {
        // Can return summary without loading heavy report
        return $"Shipment Report for {_shipmentId} (via Proxy - not loaded)";
    }
}
