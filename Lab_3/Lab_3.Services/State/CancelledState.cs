using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.State;

/// <summary>
///     Cancelled state - Terminal state for cancelled shipments
///     Concrete state implementation
/// </summary>
public class CancelledState : IShipmentState
{
    public string StateName => "Cancelled";
    public string[] AllowedActions => new[] { "Process" };

    public void Process(IShipmentContext context)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  [{StateName}] Shipment has been cancelled");
        Console.WriteLine($"  [{StateName}] Cancellation timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($"  [{StateName}] Refund process initiated");
        Console.WriteLine($"  [{StateName}] This is a terminal state");
        Console.ResetColor();
    }

    public void MoveToNext(IShipmentContext context)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  [{StateName}] ERROR: Cannot move from cancelled state");
        Console.WriteLine($"  [{StateName}] Create a new shipment instead");
        Console.ResetColor();
    }

    public void Cancel(IShipmentContext context)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  [{StateName}] Shipment is already cancelled");
        Console.ResetColor();
    }
}
