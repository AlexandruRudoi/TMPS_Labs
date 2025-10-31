using Lab_1.Domain.Entities;
using Lab_1.Domain.Entities.Vehicles;
using Lab_1.Domain.Enums;

namespace Lab_1.Domain.Factory;

/// <summary>
///     Abstract Factory pattern interface for creating families of related logistics objects
///     Supports region-specific vehicle and driver creation with regional rules
/// </summary>
public interface ILogisticsFactory
{
    /// <summary>
    ///     Creates a standard vehicle appropriate for the region
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="licensePlate">License plate or registration</param>
    /// <returns>Configured standard vehicle instance</returns>
    Vehicle CreateStandardVehicle(string id, string licensePlate);

    /// <summary>
    ///     Creates a heavy-duty vehicle appropriate for the region
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="licensePlate">License plate or registration</param>
    /// <returns>Configured heavy vehicle instance</returns>
    Vehicle CreateHeavyVehicle(string id, string licensePlate);

    /// <summary>
    ///     Creates a light-duty vehicle appropriate for the region
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="licensePlate">License plate or registration</param>
    /// <returns>Configured light vehicle instance</returns>
    Vehicle CreateLightVehicle(string id, string licensePlate);

    /// <summary>
    ///     Creates a driver for the region
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="name">Driver's full name</param>
    /// <param name="licenseType">License type held</param>
    /// <returns>Configured driver instance</returns>
    Driver CreateDriver(string id, string name, DriverLicenseType licenseType);

    /// <summary>
    ///     Gets the region name
    /// </summary>
    /// <returns>Region identifier</returns>
    string GetRegion();

    /// <summary>
    ///     Gets the regional fuel cost multiplier
    /// </summary>
    /// <returns>Fuel cost multiplier for the region</returns>
    decimal GetRegionalFuelMultiplier();
}