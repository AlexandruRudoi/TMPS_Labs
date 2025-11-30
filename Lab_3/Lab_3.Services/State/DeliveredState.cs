using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.State;

/// <summary>
///     Delivered state - Final successful state
///     Concrete state implementation
/// </summary>
public class DeliveredState : IShipmentState
{
    public string StateName => "Delivered";
    public string[] AllowedActions => new[] { "Process" };

    public void Process(ShipmentContext context)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  [{StateName}] Shipment successfully delivered!");
        Console.WriteLine($"  [{StateName}] Delivery completed at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($"  [{StateName}] Customer satisfaction survey sent");
        Console.WriteLine($"  [{StateName}] This is a terminal state");
        Console.ResetColor();
    }

    public void MoveToNext(ShipmentContext context)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  [{StateName}] Already in final state - no next state available");
        Console.ResetColor();
    }

    public void Cancel(ShipmentContext context)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  [{StateName}] ERROR: Cannot cancel delivered shipment!");
        Console.WriteLine($"  [{StateName}] Use return process instead");
        Console.ResetColor();
    }
}
