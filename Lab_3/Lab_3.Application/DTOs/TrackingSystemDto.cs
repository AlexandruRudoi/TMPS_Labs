namespace Lab_3.Application.DTOs;

/// <summary>
///     Tracking system configuration data
/// </summary>
public class TrackingSystemDto
{
    public string SystemType { get; set; } = string.Empty;
    public string SystemName { get; set; } = string.Empty;
    public string ShipmentId { get; set; } = string.Empty;
    public List<TrackingEntryDto> TrackingEntries { get; set; } = new();
    public int EstimatedDeliveryHours { get; set; }
}
