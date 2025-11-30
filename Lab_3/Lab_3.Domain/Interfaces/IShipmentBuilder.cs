using Lab_3.Domain.Entities;
using Lab_3.Domain.Entities.Vehicles;

namespace Lab_3.Domain.Interfaces;

/// <summary>
///     Builder pattern interface for constructing complex Shipment objects
/// </summary>
public interface IShipmentBuilder
{
    /// <summary>
    ///     Sets the shipment identifier
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <returns>Builder instance for chaining</returns>
    IShipmentBuilder SetId(string id);

    /// <summary>
    ///     Adds a single package to the shipment
    /// </summary>
    /// <param name="package">Package to add</param>
    /// <returns>Builder instance for chaining</returns>
    IShipmentBuilder AddPackage(Package package);

    /// <summary>
    ///     Adds multiple packages to the shipment
    /// </summary>
    /// <param name="packages">Collection of packages to add</param>
    /// <returns>Builder instance for chaining</returns>
    IShipmentBuilder AddPackages(IEnumerable<Package> packages);

    /// <summary>
    ///     Assigns a vehicle to the shipment
    /// </summary>
    /// <param name="vehicle">Vehicle to assign</param>
    /// <returns>Builder instance for chaining</returns>
    IShipmentBuilder AssignVehicle(Vehicle vehicle);

    /// <summary>
    ///     Assigns a driver to the shipment
    /// </summary>
    /// <param name="driver">Driver to assign</param>
    /// <returns>Builder instance for chaining</returns>
    IShipmentBuilder AssignDriver(Driver driver);

    /// <summary>
    ///     Sets the delivery route
    /// </summary>
    /// <param name="route">Route to use</param>
    /// <returns>Builder instance for chaining</returns>
    IShipmentBuilder SetRoute(Route route);

    /// <summary>
    ///     Sets the scheduled delivery date
    /// </summary>
    /// <param name="date">Scheduled date</param>
    /// <returns>Builder instance for chaining</returns>
    IShipmentBuilder SetScheduledDate(DateTime date);

    /// <summary>
    ///     Adds notes or instructions to the shipment
    /// </summary>
    /// <param name="notes">Notes text</param>
    /// <returns>Builder instance for chaining</returns>
    IShipmentBuilder AddNotes(string notes);

    /// <summary>
    ///     Builds and returns the configured shipment
    /// </summary>
    /// <returns>Configured shipment instance</returns>
    Shipment Build();

    /// <summary>
    ///     Resets the builder to create a new shipment
    /// </summary>
    void Reset();
}