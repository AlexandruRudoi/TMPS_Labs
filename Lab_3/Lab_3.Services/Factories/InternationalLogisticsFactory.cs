using Lab_3.Domain.Entities;
using Lab_3.Domain.Entities.Vehicles;
using Lab_3.Domain.Enums;
using Lab_3.Domain.Factory;

namespace Lab_3.Services.Factories;

/// <summary>
///     International logistics factory for sea and air freight operations
/// </summary>
public class InternationalLogisticsFactory : ILogisticsFactory
{
    private const string Region = "International";

    /// <inheritdoc />
    public Vehicle CreateStandardVehicle(string id, string licensePlate)
    {
        return new CargoShip(id, licensePlate, 100000m, Region, containerCapacity: 200);
    }

    /// <inheritdoc />
    public Vehicle CreateHeavyVehicle(string id, string licensePlate)
    {
        return new ContainerVessel(id, licensePlate, 250000m, Region, teuCapacity: 1000);
    }

    /// <inheritdoc />
    public Vehicle CreateLightVehicle(string id, string licensePlate)
    {
        return new CargoPlane(id, licensePlate, 75000m, Region, "Boeing 777F");
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
        return 0.8m;
    }
}