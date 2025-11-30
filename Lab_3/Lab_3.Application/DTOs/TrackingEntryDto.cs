namespace Lab_3.Application.DTOs;

/// <summary>
///     Individual tracking entry from tracking system
/// </summary>
public class TrackingEntryDto
{
    public DateTime Timestamp { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public Dictionary<string, object>? Coordinates { get; set; }
    public string? TagId { get; set; }
    public string? Barcode { get; set; }
}
