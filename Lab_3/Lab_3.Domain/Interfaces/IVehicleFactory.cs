using Lab_3.Domain.Entities.Vehicles;

namespace Lab_3.Domain.Interfaces;

/// <summary>
///     Factory Method pattern interface for creating vehicles
/// </summary>
public interface IVehicleFactory
{
    /// <summary>
    ///     Creates a delivery truck instance
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="licensePlate">License plate number</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="hasRefrigeration">Indicates if truck has refrigeration</param>
    /// <returns>Configured delivery truck instance</returns>
    Vehicle CreateDeliveryTruck(string id, string licensePlate, decimal capacity, bool hasRefrigeration = false);

    /// <summary>
    ///     Creates a cargo truck instance
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="licensePlate">License plate number</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="axleCount">Number of axles</param>
    /// <returns>Configured cargo truck instance</returns>
    Vehicle CreateCargoTruck(string id, string licensePlate, decimal capacity, int axleCount = 3);

    /// <summary>
    ///     Creates a cargo ship instance
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="vesselName">Name of the vessel</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="containerCapacity">Maximum container capacity</param>
    /// <returns>Configured cargo ship instance</returns>
    Vehicle CreateCargoShip(string id, string vesselName, decimal capacity, int containerCapacity = 100);

    /// <summary>
    ///     Creates a container vessel instance
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="vesselName">Name of the vessel</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="teuCapacity">Capacity in TEU units</param>
    /// <returns>Configured container vessel instance</returns>
    Vehicle CreateContainerVessel(string id, string vesselName, decimal capacity, int teuCapacity = 500);

    /// <summary>
    ///     Creates a cargo plane instance
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="registration">Aircraft registration number</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="model">Aircraft model designation</param>
    /// <returns>Configured cargo plane instance</returns>
    Vehicle CreateCargoPlane(string id, string registration, decimal capacity, string model = "Boeing 747F");

    /// <summary>
    ///     Creates a drone instance
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="serialNumber">Drone serial number</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="isAutonomous">Indicates if drone is autonomous</param>
    /// <returns>Configured drone instance</returns>
    Vehicle CreateDrone(string id, string serialNumber, decimal capacity, bool isAutonomous = true);
}