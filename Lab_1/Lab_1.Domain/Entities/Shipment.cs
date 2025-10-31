using Lab_1.Domain.Enums;
using Lab_1.Domain.Entities.Vehicles;

namespace Lab_1.Domain.Entities;

/// <summary>
///     Complex entity created using Builder pattern
/// </summary>
public class Shipment
{
    /// <summary>
    ///     Gets or sets the unique identifier for the shipment
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    ///     Gets or sets the list of packages in this shipment
    /// </summary>
    public List<Package> Packages { get; set; }
    
    /// <summary>
    ///     Gets or sets the assigned vehicle for transport
    /// </summary>
    public Vehicle AssignedVehicle { get; set; }
    
    /// <summary>
    ///     Gets or sets the assigned driver for transport
    /// </summary>
    public Driver AssignedDriver { get; set; }
    
    /// <summary>
    ///     Gets or sets the delivery route
    /// </summary>
    public Route Route { get; set; }
    
    /// <summary>
    ///     Gets or sets the scheduled delivery date
    /// </summary>
    public DateTime ScheduledDate { get; set; }
    
    /// <summary>
    ///     Gets or sets the current shipment status
    /// </summary>
    public ShipmentStatus Status { get; set; }
    
    /// <summary>
    ///     Gets or sets the total weight of all packages
    /// </summary>
    public decimal TotalWeight { get; set; }
    
    /// <summary>
    ///     Gets or sets additional notes or instructions
    /// </summary>
    public string Notes { get; set; }

    /// <summary>
    ///     Initializes a new instance of the Shipment class
    /// </summary>
    public Shipment()
    {
        Packages = new List<Package>();
        Status = ShipmentStatus.Pending;
    }

    /// <summary>
    ///     Calculates and updates the total weight from all packages
    /// </summary>
    public void CalculateTotalWeight()
    {
        TotalWeight = Packages.Sum(p => p.Weight);
    }

    /// <summary>
    ///     Gets formatted information about the shipment
    /// </summary>
    /// <returns>String representation of shipment details</returns>
    public string GetShipmentInfo()
    {
        return $"Shipment {Id}: {Packages.Count} packages, {TotalWeight}kg total - Status: {Status}";
    }
}
