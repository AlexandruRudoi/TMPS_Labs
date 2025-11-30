using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.State;

/// <summary>
///     Out-for-Delivery state - Shipment is with delivery driver
///     Concrete state implementation
/// </summary>
public class OutForDeliveryState : IShipmentState
{
    public string StateName => "Out-for-Delivery";
    public string[] AllowedActions => new[] { "Process", "MoveToNext" };

    public void Process(ShipmentContext context)
    {
        Console.WriteLine($"  [{StateName}] Driver has shipment on delivery route...");
        Console.WriteLine($"  [{StateName}] Next stop in 15 minutes...");
        Console.WriteLine($"  [{StateName}] Customer notification sent");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  [{StateName}] Ready for final delivery");
        Console.ResetColor();
    }

    public void MoveToNext(ShipmentContext context)
    {
        Console.WriteLine($"  [{StateName}] Customer signature received!");
        Console.WriteLine($"  [{StateName}] Moving to Delivered state...");
        context.SetState(new DeliveredState());
    }

    public void Cancel(ShipmentContext context)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  [{StateName}] ERROR: Cannot cancel shipment - already out for delivery!");
        Console.WriteLine($"  [{StateName}] Contact customer service for return process");
        Console.ResetColor();
    }
}
