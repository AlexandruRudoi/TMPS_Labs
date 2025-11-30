namespace Lab_3.Domain.Interfaces;

/// <summary>
///     State interface for shipment lifecycle
///     Part of the State behavioral pattern
/// </summary>
public interface IShipmentState
{
    /// <summary>
    ///     Process the shipment in current state
    /// </summary>
    void Process(IShipmentContext context);

    /// <summary>
    ///     Move to next state in the lifecycle
    /// </summary>
    void MoveToNext(IShipmentContext context);

    /// <summary>
    ///     Cancel the shipment from current state
    /// </summary>
    void Cancel(IShipmentContext context);

    /// <summary>
    ///     Get state name
    /// </summary>
    string StateName { get; }

    /// <summary>
    ///     Get allowed actions in this state
    /// </summary>
    string[] AllowedActions { get; }
}
