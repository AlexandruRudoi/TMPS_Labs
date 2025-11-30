using Lab_3.Domain.Enums;

namespace Lab_3.Domain.Entities;

/// <summary>
///     Driver entity representing a vehicle operator
/// </summary>
public class Driver
{
    /// <summary>
    ///     Gets or sets the unique identifier for the driver
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     Gets or sets the driver's full name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the operational region
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    ///     Gets or sets the type of license held by the driver
    /// </summary>
    public DriverLicenseType LicenseType { get; set; }

    /// <summary>
    ///     Gets or sets the current status of the driver
    /// </summary>
    public DriverStatus Status { get; set; }

    /// <summary>
    ///     Initializes a new instance of the Driver class
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="name">Driver's full name</param>
    /// <param name="region">Operational region</param>
    /// <param name="licenseType">License type held</param>
    public Driver(string id, string name, string region, DriverLicenseType licenseType)
    {
        Id = id;
        Name = name;
        Region = region;
        LicenseType = licenseType;
        Status = DriverStatus.Available;
    }

    /// <summary>
    ///     Determines if the driver can operate a specific vehicle type
    /// </summary>
    /// <param name="vehicleType">The type of vehicle to check</param>
    /// <returns>True if driver is licensed to operate the vehicle; otherwise false</returns>
    public bool CanOperate(VehicleType vehicleType)
    {
        return vehicleType switch
        {
            VehicleType.Drone => LicenseType >= DriverLicenseType.Drone,
            VehicleType.DeliveryTruck => LicenseType >= DriverLicenseType.Delivery,
            VehicleType.CargoTruck => LicenseType >= DriverLicenseType.Commercial,
            VehicleType.CargoShip => LicenseType >= DriverLicenseType.Maritime,
            VehicleType.ContainerVessel => LicenseType >= DriverLicenseType.Maritime,
            VehicleType.CargoPlane => LicenseType >= DriverLicenseType.Aviation,
            _ => false
        };
    }
}