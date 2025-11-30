using Lab_3.Domain.Enums;

namespace Lab_3.Domain.Entities.Vehicles;

/// <summary>
///     Cargo Ship for maritime transport
/// </summary>
public class CargoShip : Vehicle
{
    /// <summary>
    ///     Gets or sets the maximum number of containers the ship can carry
    /// </summary>
    public int ContainerCapacity { get; set; }

    /// <summary>
    ///     Gets or sets the type of vessel
    /// </summary>
    public string VesselType { get; set; }

    /// <summary>
    ///     Initializes a new instance of the CargoShip class
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="vesselName">Name of the vessel</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="region">Operational region</param>
    /// <param name="containerCapacity">Maximum container capacity</param>
    public CargoShip(string id, string vesselName, decimal capacity, string region, int containerCapacity = 100)
        : base(id, vesselName, VehicleType.CargoShip, capacity, region)
    {
        ContainerCapacity = containerCapacity;
        VesselType = "Bulk Carrier";
    }

    /// <inheritdoc/>
    public override decimal CalculateFuelCost(decimal distance)
    {
        return distance * 2.50m;
    }

    /// <inheritdoc/>
    public override string GetVehicleInfo()
    {
        return
            $"Cargo Ship '{LicensePlate}' - Capacity: {Capacity}kg - Region: {Region} - Containers: {ContainerCapacity} - Type: {VesselType} - Status: {Status}";
    }
}