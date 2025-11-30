namespace Lab_3.Application.DTOs;

/// <summary>
///     Root configuration model for tracking systems and shipments
/// </summary>
public class TrackingConfigurationDto
{
    public List<TrackingSystemDto> TrackingSystems { get; set; } = new();
    public List<ShipmentDto> Shipments { get; set; } = new();
}
