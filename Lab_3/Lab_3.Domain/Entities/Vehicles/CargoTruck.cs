using Lab_3.Domain.Enums;

namespace Lab_3.Domain.Entities.Vehicles;

/// <summary>
///     Heavy Cargo Truck for long-haul ground transport
/// </summary>
public class CargoTruck : Vehicle
{
    /// <summary>
    ///     Initializes a new instance of the CargoTruck class
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="licensePlate">License plate number</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="region">Operational region</param>
    /// <param name="axleCount">Number of axles</param>
    /// <param name="hasTrailer">Indicates if truck has a trailer</param>
    public CargoTruck(string id, string licensePlate, decimal capacity, string region, int axleCount = 3,
        bool hasTrailer = true)
        : base(id, licensePlate, VehicleType.CargoTruck, capacity, region)
    {
        AxleCount = axleCount;
        HasTrailer = hasTrailer;
    }

    /// <summary>
    ///     Gets or sets the number of axles on the truck
    /// </summary>
    public int AxleCount { get; set; }

    /// <summary>
    ///     Gets or sets whether the truck has an attached trailer
    /// </summary>
    public bool HasTrailer { get; set; }

    /// <inheritdoc />
    public override decimal CalculateFuelCost(decimal distance)
    {
        var baseCost = distance * 0.35m;
        var axleCost = distance * AxleCount * 0.06m;
        var trailerCost = HasTrailer ? distance * 0.10m : 0;
        return baseCost + axleCost + trailerCost;
    }

    /// <inheritdoc />
    public override string GetVehicleInfo()
    {
        return
            $"Cargo Truck {LicensePlate} - Capacity: {Capacity}kg - Region: {Region} - Axles: {AxleCount} - Trailer: {HasTrailer} - Status: {Status}";
    }
}