using Lab_2.Domain.Enums;

namespace Lab_2.Domain.Entities.Vehicles;

/// <summary>
///     Container Vessel for large-scale maritime transport
/// </summary>
public class ContainerVessel : Vehicle
{
    /// <summary>
    ///     Gets or sets the capacity in Twenty-foot Equivalent Units
    /// </summary>
    public int TEUCapacity { get; set; }

    /// <summary>
    ///     Gets or sets whether the vessel has refrigerated container capability
    /// </summary>
    public bool HasRefrigeratedContainers { get; set; }

    /// <summary>
    ///     Initializes a new instance of the ContainerVessel class
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="vesselName">Name of the vessel</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="region">Operational region</param>
    /// <param name="teuCapacity">Capacity in TEU units</param>
    public ContainerVessel(string id, string vesselName, decimal capacity, string region, int teuCapacity = 500)
        : base(id, vesselName, VehicleType.ContainerVessel, capacity, region)
    {
        TEUCapacity = teuCapacity;
        HasRefrigeratedContainers = false;
    }

    /// <inheritdoc/>
    public override decimal CalculateFuelCost(decimal distance)
    {
        var baseCost = distance * 3.00m;
        return HasRefrigeratedContainers ? baseCost + (distance * 0.50m) : baseCost;
    }

    /// <inheritdoc/>
    public override string GetVehicleInfo()
    {
        return
            $"Container Vessel '{LicensePlate}' - Capacity: {Capacity}kg - Region: {Region} - TEU: {TEUCapacity} - Refrigerated: {HasRefrigeratedContainers} - Status: {Status}";
    }
}