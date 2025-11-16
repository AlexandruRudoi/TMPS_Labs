using Lab_2.Domain.Interfaces;

namespace Lab_2.Services.Decorators;

/// <summary>
///     Concrete Decorator - Adds fragile handling to shipment
/// </summary>
public class FragileHandlingDecorator : ShipmentDecorator
{
    private readonly decimal _handlingFee;
    private readonly string _specialInstructions;

    /// <summary>
    ///     Initializes a new instance of the FragileHandlingDecorator
    /// </summary>
    /// <param name="shipment">The shipment requiring fragile handling</param>
    /// <param name="specialInstructions">Special handling instructions</param>
    public FragileHandlingDecorator(IShipmentComponent shipment, string specialInstructions = "Handle with extreme care")
        : base(shipment)
    {
        _specialInstructions = specialInstructions;
        _handlingFee = 12.0m;
    }

    /// <inheritdoc />
    public override string GetDescription()
    {
        return $"{base.GetDescription()} + Fragile Handling";
    }

    /// <inheritdoc />
    public override decimal CalculateCost()
    {
        return base.CalculateCost() + _handlingFee;
    }

    /// <inheritdoc />
    public override void Process()
    {
        base.Process();
        Console.WriteLine($"  Fragile handling applied (Fee: ${_handlingFee:F2})");
        Console.WriteLine($"    Instructions: {_specialInstructions}");
        Console.WriteLine($"    WARNING: FRAGILE - Use protective packaging");
    }
}
