using Lab_2.Domain.Enums;

namespace Lab_2.Domain.Entities.Vehicles;

/// <summary>
///     Base Vehicle entity representing a delivery vehicle
/// </summary>
public abstract class Vehicle
{
    /// <summary>
    ///     Gets or sets the unique identifier for the vehicle
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     Gets or sets the license plate or registration number
    /// </summary>
    public string LicensePlate { get; set; }

    /// <summary>
    ///     Gets or sets the type of vehicle
    /// </summary>
    public VehicleType Type { get; set; }

    /// <summary>
    ///     Gets or sets the maximum cargo capacity in kilograms
    /// </summary>
    public decimal Capacity { get; set; }

    /// <summary>
    ///     Gets or sets the operational region
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    ///     Gets or sets the current status of the vehicle
    /// </summary>
    public VehicleStatus Status { get; set; }

    /// <summary>
    ///     Initializes a new instance of the Vehicle class
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="licensePlate">License plate or registration</param>
    /// <param name="type">Type of vehicle</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="region">Operational region</param>
    protected Vehicle(string id, string licensePlate, VehicleType type, decimal capacity, string region)
    {
        Id = id;
        LicensePlate = licensePlate;
        Type = type;
        Capacity = capacity;
        Region = region;
        Status = VehicleStatus.Available;
    }

    /// <summary>
    ///     Calculates the fuel cost for a given distance
    /// </summary>
    /// <param name="distance">Distance in kilometers or nautical miles</param>
    /// <returns>Total fuel cost in dollars</returns>
    public abstract decimal CalculateFuelCost(decimal distance);

    /// <summary>
    ///     Gets formatted information about the vehicle
    /// </summary>
    /// <returns>String representation of vehicle details</returns>
    public abstract string GetVehicleInfo();

    /// <summary>
    ///     Creates a shallow copy of the vehicle
    /// </summary>
    /// <returns>Cloned vehicle instance</returns>
    public Vehicle Clone()
    {
        return (Vehicle)this.MemberwiseClone();
    }
}