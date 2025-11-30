namespace Lab_3.Application.DTOs;

/// <summary>
///     Package configuration data
/// </summary>
public class PackageDto
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public bool RequiresRefrigeration { get; set; }
}