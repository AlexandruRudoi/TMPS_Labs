using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Bridge;

/// <summary>
///     Concrete Implementation for Bridge pattern
///     Autonomous control system for vehicles
/// </summary>
public class AutonomousControl : IVehicleControl
{
    public string GetControlType()
    {
        return "Autonomous Control";
    }

    public string Start()
    {
        return "Autonomous: AI initializing sensors, GPS, and navigation system";
    }

    public string Stop()
    {
        return "Autonomous: AI calculating safe stop position, executing stop sequence";
    }

    public string Accelerate()
    {
        return "Autonomous: AI calculating optimal speed based on traffic and route";
    }

    public string Brake()
    {
        return "Autonomous: AI detecting obstacles, applying adaptive braking";
    }
}