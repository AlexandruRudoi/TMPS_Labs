using Lab_3.Domain.Entities;
using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Decorators;

/// <summary>
///     Concrete Component for Decorator pattern
///     Base shipment without any enhancements
/// </summary>
public class BasicShipmentComponent : IShipmentComponent
{
    private readonly decimal _baseCost;
    private readonly Shipment _shipment;

    /// <summary>
    ///     Initializes a new instance of the BasicShipmentComponent
    /// </summary>
    /// <param name="shipment">The shipment to wrap</param>
    public BasicShipmentComponent(Shipment shipment)
    {
        _shipment = shipment;
        _baseCost = CalculateBaseCost();
    }

    /// <inheritdoc />
    public string GetId()
    {
        return _shipment.Id;
    }

    /// <inheritdoc />
    public string GetDescription()
    {
        return "Basic Shipment";
    }

    /// <inheritdoc />
    public decimal CalculateCost()
    {
        return _baseCost;
    }

    /// <inheritdoc />
    public void Process()
    {
        Console.WriteLine($"Processing basic shipment: {_shipment.Id}");
    }

    /// <inheritdoc />
    public Shipment GetShipment()
    {
        return _shipment;
    }

    /// <summary>
    ///     Calculates base shipping cost based on weight and distance
    /// </summary>
    private decimal CalculateBaseCost()
    {
        const decimal costPerKg = 2.5m;
        const decimal costPerKm = 0.5m;

        var weightCost = _shipment.TotalWeight * costPerKg;
        var distanceCost = _shipment.Route?.TotalDistance ?? 0 * costPerKm;

        return weightCost + distanceCost;
    }
}