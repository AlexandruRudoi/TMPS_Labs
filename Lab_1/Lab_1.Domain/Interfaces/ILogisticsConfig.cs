namespace Lab_1.Domain.Interfaces;

/// <summary>
///     Singleton pattern interface for centralized configuration management
/// </summary>
public interface ILogisticsConfig
{
    /// <summary>
    ///     Gets the company name
    /// </summary>
    string CompanyName { get; }

    /// <summary>
    ///     Gets the headquarters region
    /// </summary>
    string HeadquartersRegion { get; }

    /// <summary>
    ///     Gets the maximum daily distance per vehicle
    /// </summary>
    decimal MaxDailyDistance { get; }

    /// <summary>
    ///     Gets the maximum number of packages per shipment
    /// </summary>
    int MaxPackagesPerShipment { get; }

    /// <summary>
    ///     Gets the list of supported regions
    /// </summary>
    List<string> SupportedRegions { get; }

    /// <summary>
    ///     Displays the current configuration settings
    /// </summary>
    void DisplayConfiguration();
}