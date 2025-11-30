namespace Lab_3.Application.DTOs;

/// <summary>
///     Shipment configuration data
/// </summary>
public class ShipmentDto
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public List<PackageDto> Packages { get; set; } = new();
    public string VehicleId { get; set; } = string.Empty;
    public string DriverId { get; set; } = string.Empty;
    public string RouteId { get; set; } = string.Empty;
    public int ScheduledHours { get; set; }
    public decimal InsuranceValue { get; set; }
    public string Notes { get; set; } = string.Empty;
}