namespace Lab_3.Services.Adapters;

/// <summary>
///     Barcode Scanner System (Adaptee)
///     Third-party barcode tracking with XML-based interface
/// </summary>
public class BarcodeScanner
{
    /// <summary>
    ///     Scans barcode and returns XML data
    /// </summary>
    public string ScanBarcode(string barcodeNumber)
    {
        return $@"
            <scan>
                <barcode>{barcodeNumber}</barcode>
                <facility>Regional Hub - Sector 7</facility>
                <timestamp>{DateTime.Now:yyyy-MM-ddTHH:mm:ss}</timestamp>
                <operator>Scanner-05</operator>
            </scan>";
    }
    
    /// <summary>
    ///     Gets scan history as XML
    /// </summary>
    public string GetBarcodeHistory(string barcodeNumber)
    {
        return $@"
            <history>
                <scan><facility>Origin Facility</facility><time>{DateTime.Now.AddHours(-5):HH:mm}</time></scan>
                <scan><facility>Transit Hub Alpha</facility><time>{DateTime.Now.AddHours(-3):HH:mm}</time></scan>
                <scan><facility>Regional Hub - Sector 7</facility><time>{DateTime.Now:HH:mm}</time></scan>
            </history>";
    }
    
    /// <summary>
    ///     Estimates delivery in hours
    /// </summary>
    public double EstimateDeliveryHours(string barcodeNumber)
    {
        return 4.5; // Hours from now
    }
}
