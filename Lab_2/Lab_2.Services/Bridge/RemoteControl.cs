using Lab_2.Domain.Interfaces;

namespace Lab_2.Services.Bridge;

/// <summary>
///     Concrete Implementation for Bridge pattern
///     Remote control system for vehicles (drones, ships)
/// </summary>
public class RemoteControl : IVehicleControl
{
    private readonly string _operatorId;

    public RemoteControl(string operatorId = "REMOTE-OP-001")
    {
        _operatorId = operatorId;
    }

    public string GetControlType() => $"Remote Control (Operator: {_operatorId})";

    public string Start()
    {
        return $"Remote: Operator {_operatorId} sending start command via secure connection";
    }

    public string Stop()
    {
        return $"Remote: Operator {_operatorId} initiating controlled shutdown";
    }

    public string Accelerate()
    {
        return $"Remote: Operator {_operatorId} adjusting throttle remotely";
    }

    public string Brake()
    {
        return $"Remote: Operator {_operatorId} applying remote braking system";
    }
}
