using Lab_2.Domain.Entities;
using Lab_2.Domain.Entities.Vehicles;
using Lab_2.Domain.Enums;
using Lab_2.Domain.Factory;

namespace Lab_2.Services.Factories;

/// <summary>
///     Rural region logistics factory for countryside delivery operations
/// </summary>
public class RuralLogisticsFactory : ILogisticsFactory
{
    private const string Region = "Rural";

    /// <inheritdoc />
    public Vehicle CreateStandardVehicle(string id, string licensePlate)
    {
        return new CargoTruck(id, licensePlate, 8000m, Region, axleCount: 4, hasTrailer: true);
    }

    /// <inheritdoc />
    public Vehicle CreateHeavyVehicle(string id, string licensePlate)
    {
        return new CargoTruck(id, licensePlate, 15000m, Region, axleCount: 5, hasTrailer: true);
    }

    /// <inheritdoc />
    public Vehicle CreateLightVehicle(string id, string licensePlate)
    {
        return new DeliveryTruck(id, licensePlate, 2000m, Region, hasRefrigeration: true);
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
        return 0.9m;
    }
}