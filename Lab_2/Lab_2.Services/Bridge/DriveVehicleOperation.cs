using Lab_2.Domain.Interfaces;

namespace Lab_2.Services.Bridge;

/// <summary>
///     Refined Abstraction for Bridge pattern
///     Driving operation with acceleration and braking
/// </summary>
public class DriveVehicleOperation : VehicleOperation
{
    public DriveVehicleOperation(IVehicleControl control, string vehicleId) 
        : base(control, vehicleId)
    {
    }

    public override void Execute()
    {
        Console.WriteLine($"\n Driving Vehicle {_vehicleId}");
        Console.WriteLine($"   Control System: {_control.GetControlType()}");
        Console.WriteLine($"   Accelerating: {_control.Accelerate()}");
        Console.WriteLine($"   Braking: {_control.Brake()}");
    }
}
