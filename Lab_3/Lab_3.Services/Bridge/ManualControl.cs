using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Bridge;

/// <summary>
///     Concrete Implementation for Bridge pattern
///     Manual control system for vehicles
/// </summary>
public class ManualControl : IVehicleControl
{
    public string GetControlType() => "Manual Control";

    public string Start()
    {
        return "Manual: Turn key, press clutch, shift to neutral, start engine";
    }

    public string Stop()
    {
        return "Manual: Press brake, shift to neutral, turn off engine";
    }

    public string Accelerate()
    {
        return "Manual: Press clutch, shift gear up, release clutch, press accelerator";
    }

    public string Brake()
    {
        return "Manual: Release accelerator, press brake pedal";
    }
}
