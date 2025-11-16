using Lab_2.Domain.Interfaces;

namespace Lab_2.Services.Decorators;

/// <summary>
///     Concrete Decorator - Adds signature confirmation requirement
/// </summary>
public class SignatureConfirmationDecorator : ShipmentDecorator
{
    private readonly bool _requiresIdVerification;
    private readonly decimal _confirmationFee;

    /// <summary>
    ///     Initializes a new instance of the SignatureConfirmationDecorator
    /// </summary>
    /// <param name="shipment">The shipment requiring signature</param>
    /// <param name="requiresIdVerification">Whether ID verification is required</param>
    public SignatureConfirmationDecorator(IShipmentComponent shipment, bool requiresIdVerification = false) 
        : base(shipment)
    {
        _requiresIdVerification = requiresIdVerification;
        _confirmationFee = requiresIdVerification ? 8.0m : 5.0m;
    }

    /// <inheritdoc />
    public override string GetDescription()
    {
        var type = _requiresIdVerification ? "Signature + ID Verification" : "Signature Confirmation";
        return $"{base.GetDescription()} + {type}";
    }

    /// <inheritdoc />
    public override decimal CalculateCost()
    {
        return base.CalculateCost() + _confirmationFee;
    }

    /// <inheritdoc />
    public override void Process()
    {
        base.Process();
        var verificationType = _requiresIdVerification ? "with ID verification" : "required";
        Console.WriteLine($"  Signature confirmation {verificationType} (Fee: ${_confirmationFee:F2})");
    }
}
