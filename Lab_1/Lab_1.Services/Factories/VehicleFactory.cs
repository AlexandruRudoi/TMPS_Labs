using Lab_1.Domain.Entities.Vehicles;
using Lab_1.Domain.Enums;
using Lab_1.Domain.Interfaces;

namespace Lab_1.Services.Factories;

/// <summary>
///     Factory Method pattern implementation for creating vehicles
/// </summary>
public class VehicleFactory : IVehicleFactory
{
    /// <summary>
    ///     Region where vehicles will operate
    /// </summary>
    private readonly string _region;

    /// <summary>
    ///     Initializes a new instance of the VehicleFactory
    /// </summary>
    /// <param name="region">Operating region for created vehicles</param>
    public VehicleFactory(string region)
    {
        _region = region;
    }

    /// <inheritdoc />
    public Vehicle CreateDeliveryTruck(string id, string licensePlate, decimal capacity, bool hasRefrigeration = false)
    {
        return new DeliveryTruck(id, licensePlate, capacity, _region, hasRefrigeration);
    }

    /// <inheritdoc />
    public Vehicle CreateCargoTruck(string id, string licensePlate, decimal capacity, int axleCount = 3)
    {
        return new CargoTruck(id, licensePlate, capacity, _region, axleCount);
    }

    /// <inheritdoc />
    public Vehicle CreateCargoShip(string id, string vesselName, decimal capacity, int containerCapacity = 100)
    {
        return new CargoShip(id, vesselName, capacity, _region, containerCapacity);
    }

    /// <inheritdoc />
    public Vehicle CreateContainerVessel(string id, string vesselName, decimal capacity, int teuCapacity = 500)
    {
        return new ContainerVessel(id, vesselName, capacity, _region, teuCapacity);
    }

    /// <inheritdoc />
    public Vehicle CreateCargoPlane(string id, string registration, decimal capacity, string model = "Boeing 747F")
    {
        return new CargoPlane(id, registration, capacity, _region, model);
    }

    /// <inheritdoc />
    public Vehicle CreateDrone(string id, string serialNumber, decimal capacity, bool isAutonomous = true)
    {
        return new Drone(id, serialNumber, capacity, _region, isAutonomous);
    }
}
