namespace Lab_3.Domain.Interfaces;

/// <summary>
///     Context interface for State pattern
///     Allows states to interact with the context without circular dependency
/// </summary>
public interface IShipmentContext
{
    string ShipmentId { get; }
    DateTime CreatedAt { get; }
    List<string> StateHistory { get; }
    
    void SetState(IShipmentState newState);
    IShipmentState GetCurrentState();
}
