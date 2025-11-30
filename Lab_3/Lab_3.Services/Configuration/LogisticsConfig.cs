using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Configuration;

/// <summary>
///     Singleton pattern implementation for centralized logistics configuration
/// </summary>
public sealed class LogisticsConfig : ILogisticsConfig
{
    /// <summary>
    ///     Thread-safe lazy initialization of singleton instance
    /// </summary>
    private static readonly Lazy<LogisticsConfig> _instance =
        new Lazy<LogisticsConfig>(() => new LogisticsConfig());

    /// <summary>
    ///     Gets the singleton instance of LogisticsConfig
    /// </summary>
    public static LogisticsConfig Instance => _instance.Value;

    /// <inheritdoc />
    public string CompanyName { get; private set; }

    /// <inheritdoc />
    public string HeadquartersRegion { get; private set; }

    /// <inheritdoc />
    public decimal MaxDailyDistance { get; private set; }

    /// <inheritdoc />
    public int MaxPackagesPerShipment { get; private set; }

    /// <inheritdoc />
    public List<string> SupportedRegions { get; private set; }

    /// <summary>
    ///     Private constructor prevents external instantiation
    /// </summary>
    private LogisticsConfig()
    {
        CompanyName = "FastTrack Logistics";
        HeadquartersRegion = "Urban";
        MaxDailyDistance = 500m;
        MaxPackagesPerShipment = 50;
        SupportedRegions = new List<string> { "Urban", "Rural", "Suburban" };
    }

    /// <inheritdoc />
    public void DisplayConfiguration()
    {
        Console.WriteLine("=== Logistics System Configuration ===");
        Console.WriteLine($"Company: {CompanyName}");
        Console.WriteLine($"Headquarters: {HeadquartersRegion}");
        Console.WriteLine($"Max Daily Distance: {MaxDailyDistance}km");
        Console.WriteLine($"Max Packages per Shipment: {MaxPackagesPerShipment}");
        Console.WriteLine($"Supported Regions: {string.Join(", ", SupportedRegions)}");
        Console.WriteLine("======================================\n");
    }

    /// <summary>
    ///     Updates the maximum daily distance limit
    /// </summary>
    /// <param name="distance">New maximum distance in kilometers</param>
    public void UpdateMaxDailyDistance(decimal distance)
    {
        if (distance > 0)
            MaxDailyDistance = distance;
    }

    /// <summary>
    ///     Updates the maximum packages per shipment limit
    /// </summary>
    /// <param name="maxPackages">New maximum package count</param>
    public void UpdateMaxPackagesPerShipment(int maxPackages)
    {
        if (maxPackages > 0)
            MaxPackagesPerShipment = maxPackages;
    }

    /// <summary>
    ///     Adds a new supported region if not already present
    /// </summary>
    /// <param name="region">Region name to add</param>
    public void AddSupportedRegion(string region)
    {
        if (!string.IsNullOrWhiteSpace(region) && !SupportedRegions.Contains(region))
            SupportedRegions.Add(region);
    }
}