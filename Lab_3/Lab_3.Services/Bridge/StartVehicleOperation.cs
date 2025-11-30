using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Bridge;

/// <summary>
///     Refined Abstraction for Bridge pattern
///     Start operation
/// </summary>
public class StartVehicleOperation : VehicleOperation
{
    public StartVehicleOperation(IVehicleControl control, string vehicleId)
        : base(control, vehicleId)
    {
    }

    public override void Execute()
    {
        Console.WriteLine($"\n Starting Vehicle {_vehicleId}");
        Console.WriteLine($"   Control System: {_control.GetControlType()}");
        Console.WriteLine($"   Action: {_control.Start()}");
    }
}