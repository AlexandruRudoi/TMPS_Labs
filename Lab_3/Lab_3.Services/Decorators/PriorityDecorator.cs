using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Decorators;

/// <summary>
///     Concrete Decorator - Adds priority handling to shipment
/// </summary>
public class PriorityDecorator : ShipmentDecorator
{
    private readonly decimal _priorityFee;
    private readonly string _priorityLevel;

    /// <summary>
    ///     Initializes a new instance of the PriorityDecorator
    /// </summary>
    /// <param name="shipment">The shipment to prioritize</param>
    /// <param name="priorityLevel">Priority level (Express, Urgent, Standard)</param>
    public PriorityDecorator(IShipmentComponent shipment, string priorityLevel = "Express")
        : base(shipment)
    {
        _priorityLevel = priorityLevel;
        _priorityFee = priorityLevel switch
        {
            "Urgent" => 50.0m,
            "Express" => 25.0m,
            "Standard" => 10.0m,
            _ => 0m
        };
    }

    /// <inheritdoc />
    public override string GetDescription()
    {
        return $"{base.GetDescription()} + {_priorityLevel} Priority";
    }

    /// <inheritdoc />
    public override decimal CalculateCost()
    {
        return base.CalculateCost() + _priorityFee;
    }

    /// <inheritdoc />
    public override void Process()
    {
        base.Process();
        Console.WriteLine($"  Priority handling: {_priorityLevel} (Fee: ${_priorityFee:F2})");
    }
}