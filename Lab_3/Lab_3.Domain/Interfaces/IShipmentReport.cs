namespace Lab_3.Domain.Interfaces;

/// <summary>
///     Subject interface for Proxy pattern
///     Defines operations for shipment reports
/// </summary>
public interface IShipmentReport
{
    /// <summary>
    ///     Loads the complete shipment report
    /// </summary>
    void Load();
    
    /// <summary>
    ///     Displays the report
    /// </summary>
    void Display();
    
    /// <summary>
    ///     Exports report to PDF
    /// </summary>
    string ExportToPdf();
    
    /// <summary>
    ///     Gets report summary
    /// </summary>
    string GetSummary();
}
