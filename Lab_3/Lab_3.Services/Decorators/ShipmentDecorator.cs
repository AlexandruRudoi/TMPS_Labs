using Lab_3.Domain.Entities;
using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Decorators;

/// <summary>
///     Base Decorator for shipment enhancements
///     Wraps IShipmentComponent and delegates to it
/// </summary>
public abstract class ShipmentDecorator : IShipmentComponent
{
    protected readonly IShipmentComponent _wrappedShipment;

    /// <summary>
    ///     Initializes a new instance of the ShipmentDecorator
    /// </summary>
    /// <param name="shipment">The shipment component to decorate</param>
    protected ShipmentDecorator(IShipmentComponent shipment)
    {
        _wrappedShipment = shipment;
    }

    /// <inheritdoc />
    public virtual string GetId() => _wrappedShipment.GetId();

    /// <inheritdoc />
    public virtual string GetDescription() => _wrappedShipment.GetDescription();

    /// <inheritdoc />
    public virtual decimal CalculateCost() => _wrappedShipment.CalculateCost();

    /// <inheritdoc />
    public virtual void Process() => _wrappedShipment.Process();

    /// <inheritdoc />
    public Shipment GetShipment() => _wrappedShipment.GetShipment();
}
