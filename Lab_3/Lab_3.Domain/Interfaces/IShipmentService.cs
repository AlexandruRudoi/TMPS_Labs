using Lab_3.Domain.Entities;
using Lab_3.Domain.Entities.Vehicles;
using Lab_3.Domain.Entities;

namespace Lab_3.Domain.Interfaces;

/// <summary>
///     Service interface for managing shipments
/// </summary>
public interface IShipmentService
{
    /// <summary>
    ///     Creates a new shipment with the specified components
    /// </summary>
    /// <param name="packages">Packages to include in shipment</param>
    /// <param name="vehicle">Assigned vehicle</param>
    /// <param name="driver">Assigned driver</param>
    /// <param name="route">Delivery route</param>
    /// <returns>Configured shipment instance</returns>
    Shipment CreateShipment(IEnumerable<Package> packages, Vehicle vehicle, Driver driver, Route route);

    /// <summary>
    ///     Validates a shipment for completeness and consistency
    /// </summary>
    /// <param name="shipment">Shipment to validate</param>
    /// <returns>True if shipment is valid; otherwise false</returns>
    bool ValidateShipment(Shipment shipment);

    /// <summary>
    ///     Displays detailed information about a shipment
    /// </summary>
    /// <param name="shipment">Shipment to display</param>
    void DisplayShipmentDetails(Shipment shipment);
}