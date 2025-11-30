using Lab_3.Domain.Enums;

namespace Lab_3.Domain.Entities.Vehicles;

/// <summary>
///     Drone for last-mile delivery and urgent small packages
/// </summary>
public class Drone : Vehicle
{
    /// <summary>
    ///     Gets or sets the battery capacity as a percentage
    /// </summary>
    public decimal BatteryCapacity { get; set; }

    /// <summary>
    ///     Gets or sets the maximum flight time in minutes
    /// </summary>
    public decimal MaxFlightTime { get; set; }

    /// <summary>
    ///     Gets or sets whether the drone operates autonomously
    /// </summary>
    public bool IsAutonomous { get; set; }

    /// <summary>
    ///     Initializes a new instance of the Drone class
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="serialNumber">Drone serial number</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="region">Operational region</param>
    /// <param name="isAutonomous">Indicates if drone is autonomous</param>
    public Drone(string id, string serialNumber, decimal capacity, string region, bool isAutonomous = true)
        : base(id, serialNumber, VehicleType.Drone, capacity, region)
    {
        BatteryCapacity = 100m;
        MaxFlightTime = 45m;
        IsAutonomous = isAutonomous;
    }

    /// <inheritdoc/>
    public override decimal CalculateFuelCost(decimal distance)
    {
        return distance * 0.05m;
    }

    /// <inheritdoc/>
    public override string GetVehicleInfo()
    {
        return
            $"Drone {LicensePlate} - Capacity: {Capacity}kg - Region: {Region} - Battery: {BatteryCapacity}% - Flight Time: {MaxFlightTime}min - Autonomous: {IsAutonomous} - Status: {Status}";
    }
}