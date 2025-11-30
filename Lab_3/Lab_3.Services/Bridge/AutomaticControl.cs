using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Bridge;

/// <summary>
///     Concrete Implementation for Bridge pattern
///     Automatic control system for vehicles
/// </summary>
public class AutomaticControl : IVehicleControl
{
    public string GetControlType() => "Automatic Control";

    public string Start()
    {
        return "Automatic: Press brake, push start button";
    }

    public string Stop()
    {
        return "Automatic: Press brake, push stop button, engage parking mode";
    }

    public string Accelerate()
    {
        return "Automatic: Press accelerator (transmission shifts automatically)";
    }

    public string Brake()
    {
        return "Automatic: Release accelerator, press brake pedal";
    }
}
