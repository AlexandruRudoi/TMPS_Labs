using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.State;

/// <summary>
///     In-Transit state - Shipment is being transported
///     Concrete state implementation
/// </summary>
public class InTransitState : IShipmentState
{
    public string StateName => "In-Transit";
    public string[] AllowedActions => new[] { "Process", "MoveToNext", "Cancel" };

    public void Process(IShipmentContext context)
    {
        Console.WriteLine($"  [{StateName}] Tracking shipment location...");
        Console.WriteLine($"  [{StateName}] Vehicle en route to destination...");
        Console.WriteLine($"  [{StateName}] ETA: 2 hours");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  [{StateName}] Ready to move to Out-for-Delivery");
        Console.ResetColor();
    }

    public void MoveToNext(IShipmentContext context)
    {
        Console.WriteLine($"  [{StateName}] Arrived at delivery hub, moving to Out-for-Delivery...");
        context.SetState(new OutForDeliveryState());
    }

    public void Cancel(IShipmentContext context)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  [{StateName}] WARNING: Cancellation requires vehicle return");
        Console.ResetColor();
        Console.WriteLine($"  [{StateName}] Initiating return process...");
        context.SetState(new CancelledState());
    }
}
