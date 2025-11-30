using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Bridge;

/// <summary>
///     Refined Abstraction for Bridge pattern
///     Stop operation
/// </summary>
public class StopVehicleOperation : VehicleOperation
{
    public StopVehicleOperation(IVehicleControl control, string vehicleId) 
        : base(control, vehicleId)
    {
    }

    public override void Execute()
    {
        Console.WriteLine($"\n Stopping Vehicle {_vehicleId}");
        Console.WriteLine($"   Control System: {_control.GetControlType()}");
        Console.WriteLine($"   Action: {_control.Stop()}");
    }
}
