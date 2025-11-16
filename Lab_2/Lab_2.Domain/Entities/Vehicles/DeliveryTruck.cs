using Lab_2.Domain.Enums;

namespace Lab_2.Domain.Entities.Vehicles;

/// <summary>
///     Delivery Truck for local/regional ground transport
/// </summary>
public class DeliveryTruck : Vehicle
{
    /// <summary>
    ///     Gets or sets whether the truck has refrigeration capability
    /// </summary>
    public bool HasRefrigeration { get; set; }

    /// <summary>
    ///     Gets or sets the maximum number of pallets that can be loaded
    /// </summary>
    public int MaxPallets { get; set; }

    /// <summary>
    ///     Initializes a new instance of the DeliveryTruck class
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="licensePlate">License plate number</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="region">Operational region</param>
    /// <param name="hasRefrigeration">Indicates if truck has refrigeration</param>
    public DeliveryTruck(string id, string licensePlate, decimal capacity, string region, bool hasRefrigeration = false)
        : base(id, licensePlate, VehicleType.DeliveryTruck, capacity, region)
    {
        HasRefrigeration = hasRefrigeration;
        MaxPallets = 12;
    }

    /// <inheritdoc/>
    public override decimal CalculateFuelCost(decimal distance)
    {
        var baseCost = distance * 0.18m;
        return HasRefrigeration ? baseCost + (distance * 0.03m) : baseCost;
    }

    /// <inheritdoc/>
    public override string GetVehicleInfo()
    {
        return
            $"Delivery Truck {LicensePlate} - Capacity: {Capacity}kg - Region: {Region} - Refrigerated: {HasRefrigeration} - Pallets: {MaxPallets} - Status: {Status}";
    }
}