using Lab_2.Domain.Interfaces;

namespace Lab_2.Services.Proxy;

/// <summary>
///     Protection Proxy for access control
///     Controls access to reports based on user permissions
/// </summary>
public class ProtectedShipmentReportProxy : IShipmentReport
{
    private readonly string _shipmentId;
    private readonly string _userId;
    private readonly string _userRole;
    private ShipmentReport _realReport;

    public ProtectedShipmentReportProxy(string shipmentId, string userId, string userRole)
    {
        _shipmentId = shipmentId;
        _userId = userId;
        _userRole = userRole;
        Console.WriteLine($"   Protected proxy created for user {_userId} (Role: {_userRole})");
    }

    public void Load()
    {
        if (!CheckAccess("Read"))
            return;

        if (_realReport == null)
            _realReport = new ShipmentReport(_shipmentId);
            
        _realReport.Load();
    }

    public void Display()
    {
        if (!CheckAccess("Read"))
        {
            Console.WriteLine($"\n     ACCESS DENIED: User {_userId} cannot view reports");
            Console.WriteLine($"      Required role: Manager or Admin");
            Console.WriteLine($"      Current role: {_userRole}\n");
            return;
        }

        if (_realReport == null)
            _realReport = new ShipmentReport(_shipmentId);
            
        _realReport.Display();
    }

    public string ExportToPdf()
    {
        if (!CheckAccess("Export"))
        {
            Console.WriteLine($"\n     ACCESS DENIED: User {_userId} cannot export reports");
            Console.WriteLine($"      Required role: Admin");
            Console.WriteLine($"      Current role: {_userRole}\n");
            return null;
        }

        if (_realReport == null)
            _realReport = new ShipmentReport(_shipmentId);
            
        return _realReport.ExportToPdf();
    }

    public string GetSummary()
    {
        // Summary available to everyone
        return $"Shipment Report for {_shipmentId} (User: {_userId}, Role: {_userRole})";
    }

    private bool CheckAccess(string operation)
    {
        return operation switch
        {
            "Read" => _userRole == "Manager" || _userRole == "Admin" || _userRole == "Viewer",
            "Export" => _userRole == "Admin",
            _ => false
        };
    }
}
