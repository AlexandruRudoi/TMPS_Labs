using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.State;

/// <summary>
///     Pending state - Initial shipment state
///     Concrete state implementation
/// </summary>
public class PendingState : IShipmentState
{
    public string StateName => "Pending";
    public string[] AllowedActions => new[] { "Process", "MoveToNext", "Cancel" };

    public void Process(IShipmentContext context)
    {
        Console.WriteLine($"  [{StateName}] Validating shipment details...");
        Console.WriteLine($"  [{StateName}] Assigning vehicle and driver...");
        Console.WriteLine($"  [{StateName}] Generating shipping labels...");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  [{StateName}] Ready to move to In-Transit");
        Console.ResetColor();
    }

    public void MoveToNext(IShipmentContext context)
    {
        Console.WriteLine($"  [{StateName}] Moving to In-Transit state...");
        context.SetState(new InTransitState());
    }

    public void Cancel(IShipmentContext context)
    {
        Console.WriteLine($"  [{StateName}] Shipment cancelled before pickup");
        context.SetState(new CancelledState());
    }
}
