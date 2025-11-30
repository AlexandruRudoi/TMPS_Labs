using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.State;

/// <summary>
///     Context class that maintains current state and delegates behavior
///     Part of the State behavioral pattern
/// </summary>
public class ShipmentContext : IShipmentContext
{
    private IShipmentState _currentState;
    public string ShipmentId { get; }
    public DateTime CreatedAt { get; }
    public List<string> StateHistory { get; } = new();

    public ShipmentContext(string shipmentId)
    {
        ShipmentId = shipmentId;
        CreatedAt = DateTime.Now;
        _currentState = new PendingState();
        StateHistory.Add($"[{DateTime.Now:HH:mm:ss}] Created in {_currentState.StateName}");
    }

    /// <summary>
    ///     Change the current state
    /// </summary>
    public void SetState(IShipmentState newState)
    {
        var oldStateName = _currentState?.StateName ?? "None";
        _currentState = newState;
        StateHistory.Add($"[{DateTime.Now:HH:mm:ss}] {oldStateName} -> {newState.StateName}");
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n[State Change] {ShipmentId}: {oldStateName} -> {newState.StateName}");
        Console.ResetColor();
    }

    /// <summary>
    ///     Get current state
    /// </summary>
    public IShipmentState GetCurrentState() => _currentState;

    /// <summary>
    ///     Process shipment using current state's behavior
    /// </summary>
    public void Process()
    {
        Console.WriteLine($"\n[{ShipmentId}] Processing in state: {_currentState.StateName}");
        _currentState.Process(this);
    }

    /// <summary>
    ///     Move to next state
    /// </summary>
    public void MoveToNext()
    {
        Console.WriteLine($"\n[{ShipmentId}] Attempting to move to next state from: {_currentState.StateName}");
        _currentState.MoveToNext(this);
    }

    /// <summary>
    ///     Cancel shipment
    /// </summary>
    public void Cancel()
    {
        Console.WriteLine($"\n[{ShipmentId}] Attempting to cancel from state: {_currentState.StateName}");
        _currentState.Cancel(this);
    }

    /// <summary>
    ///     Display current status
    /// </summary>
    public void DisplayStatus()
    {
        Console.WriteLine($"\n--- Shipment {ShipmentId} Status ---");
        Console.WriteLine($"Current State: {_currentState.StateName}");
        Console.WriteLine($"Allowed Actions: {string.Join(", ", _currentState.AllowedActions)}");
        Console.WriteLine($"Created: {CreatedAt:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($"State Changes: {StateHistory.Count}");
    }

    /// <summary>
    ///     Display state history
    /// </summary>
    public void DisplayHistory()
    {
        Console.WriteLine($"\n--- State History for {ShipmentId} ---");
        foreach (var entry in StateHistory)
        {
            Console.WriteLine($"  {entry}");
        }
    }
}
