using Lab_3.Domain.Entities;

namespace Lab_3.Domain.Interfaces;

/// <summary>
///     Component interface for Decorator pattern
///     Defines operations for shipment processing
/// </summary>
public interface IShipmentComponent
{
    /// <summary>
    ///     Gets the shipment identifier
    /// </summary>
    string GetId();

    /// <summary>
    ///     Gets the shipment description with all applied features
    /// </summary>
    string GetDescription();

    /// <summary>
    ///     Calculates the total cost including base cost and all enhancements
    /// </summary>
    decimal CalculateCost();

    /// <summary>
    ///     Processes the shipment with all applied enhancements
    /// </summary>
    void Process();

    /// <summary>
    ///     Gets the underlying shipment object
    /// </summary>
    Shipment GetShipment();
}