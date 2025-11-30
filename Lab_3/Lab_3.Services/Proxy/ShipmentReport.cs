using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Proxy;

/// <summary>
///     Real Subject for Proxy pattern
///     Heavy object that contains actual shipment report data
/// </summary>
public class ShipmentReport : IShipmentReport
{
    private readonly string _shipmentId;
    private bool _isLoaded;
    private string _reportData;

    public ShipmentReport(string shipmentId)
    {
        _shipmentId = shipmentId;
        _isLoaded = false;
    }

    public void Load()
    {
        if (!_isLoaded)
        {
            Console.WriteLine($"   Loading heavy report data for shipment {_shipmentId}...");
            Thread.Sleep(1000); // Simulate expensive database/API call

            _reportData = GenerateReportData();
            _isLoaded = true;

            Console.WriteLine($"   Report data loaded ({_reportData.Length} characters)");
        }
    }

    public void Display()
    {
        if (!_isLoaded)
            Load();

        Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  SHIPMENT REPORT: {_shipmentId,-36} ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine(_reportData);
        Console.WriteLine("═══════════════════════════════════════════════════════════\n");
    }

    public string ExportToPdf()
    {
        if (!_isLoaded)
            Load();

        Console.WriteLine("   Exporting report to PDF...");
        Thread.Sleep(500); // Simulate PDF generation
        var filename = $"Report_{_shipmentId}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
        Console.WriteLine($"   PDF exported: {filename}");
        return filename;
    }

    public string GetSummary()
    {
        // Summary doesn't require full load
        return $"Shipment Report for {_shipmentId}";
    }

    private string GenerateReportData()
    {
        return $@"
Shipment ID: {_shipmentId}
Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}

SHIPMENT DETAILS:
- Origin: Urban Distribution Center A
- Destination: Regional Hub B
- Distance: 125.5 km
- Estimated Duration: 2 hours 30 minutes

PACKAGE INFORMATION:
- Total Packages: 15
- Total Weight: 245.8 kg
- Fragile Items: 3
- Temperature Controlled: 2

VEHICLE ASSIGNMENT:
- Vehicle ID: V-2024-045
- Type: Cargo Truck
- License: CT-XYZ-789
- Capacity: 5000 kg

DRIVER INFORMATION:
- Driver ID: D-2024-123
- Name: John Anderson
- License: Commercial Class B
- Years of Experience: 8

ROUTE INFORMATION:
- Route ID: R-URBAN-045
- Type: Urban to Rural
- Waypoints: 8
- Rest Stops: 2

COST BREAKDOWN:
- Base Shipping: $125.50
- Fuel Surcharge: $45.20
- Insurance: $35.00
- Special Handling: $15.00
- Total: $220.70

STATUS HISTORY:
[{DateTime.Now.AddHours(-24):HH:mm}] Order received
[{DateTime.Now.AddHours(-20):HH:mm}] Shipment prepared
[{DateTime.Now.AddHours(-18):HH:mm}] Vehicle assigned
[{DateTime.Now.AddHours(-16):HH:mm}] Departed origin
[{DateTime.Now.AddHours(-14):HH:mm}] In transit - Checkpoint 1
[{DateTime.Now:HH:mm}] Current location - Checkpoint 4
";
    }
}