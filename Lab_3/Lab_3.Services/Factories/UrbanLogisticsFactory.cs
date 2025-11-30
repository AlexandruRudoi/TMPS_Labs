using Lab_3.Domain.Entities;
using Lab_3.Domain.Entities.Vehicles;
using Lab_3.Domain.Enums;
using Lab_3.Domain.Factory;

namespace Lab_3.Services.Factories;

/// <summary>
///     Urban region logistics factory for city-based delivery operations
/// </summary>
public class UrbanLogisticsFactory : ILogisticsFactory
{
    private const string Region = "Urban";

    /// <inheritdoc />
    public Vehicle CreateStandardVehicle(string id, string licensePlate)
    {
        return new DeliveryTruck(id, licensePlate, 1500m, Region, hasRefrigeration: false);
    }

    /// <inheritdoc />
    public Vehicle CreateHeavyVehicle(string id, string licensePlate)
    {
        return new CargoPlane(id, licensePlate, 50000m, Region, "Airbus A300F");
    }

    /// <inheritdoc />
    public Vehicle CreateLightVehicle(string id, string licensePlate)
    {
        return new Drone(id, licensePlate, 25m, Region, isAutonomous: true);
    }

    /// <inheritdoc />
    public Driver CreateDriver(string id, string name, DriverLicenseType licenseType)
    {
        return new Driver(id, name, Region, licenseType);
    }

    /// <inheritdoc />
    public string GetRegion()
    {
        return Region;
    }

    /// <inheritdoc />
    public decimal GetRegionalFuelMultiplier()
    {
        return 1.2m;
    }
}