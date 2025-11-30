using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Decorators;

/// <summary>
///     Concrete Decorator - Adds insurance coverage to shipment
/// </summary>
public class InsuranceDecorator : ShipmentDecorator
{
    private readonly decimal _insuranceValue;
    private readonly decimal _insuranceRate;

    /// <summary>
    ///     Initializes a new instance of the InsuranceDecorator
    /// </summary>
    /// <param name="shipment">The shipment to insure</param>
    /// <param name="insuranceValue">Declared value for insurance</param>
    /// <param name="insuranceRate">Insurance rate (default 2% of value)</param>
    public InsuranceDecorator(IShipmentComponent shipment, decimal insuranceValue, decimal insuranceRate = 0.02m) 
        : base(shipment)
    {
        _insuranceValue = insuranceValue;
        _insuranceRate = insuranceRate;
    }

    /// <inheritdoc />
    public override string GetDescription()
    {
        return $"{base.GetDescription()} + Insurance (${_insuranceValue:F2} coverage)";
    }

    /// <inheritdoc />
    public override decimal CalculateCost()
    {
        var insuranceFee = _insuranceValue * _insuranceRate;
        return base.CalculateCost() + insuranceFee;
    }

    /// <inheritdoc />
    public override void Process()
    {
        base.Process();
        Console.WriteLine($"  Insurance applied: ${_insuranceValue:F2} coverage (Fee: ${_insuranceValue * _insuranceRate:F2})");
    }
}
