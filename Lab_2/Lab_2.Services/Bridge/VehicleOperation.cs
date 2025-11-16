using Lab_2.Domain.Interfaces;

namespace Lab_2.Services.Bridge;

/// <summary>
///     Abstraction for Bridge pattern
///     Represents vehicle operations independent of control implementation
/// </summary>
public abstract class VehicleOperation
{
    protected readonly IVehicleControl _control;
    protected readonly string _vehicleId;

    protected VehicleOperation(IVehicleControl control, string vehicleId)
    {
        _control = control;
        _vehicleId = vehicleId;
    }

    public abstract void Execute();
    
    public virtual string GetOperationInfo()
    {
        return $"Vehicle {_vehicleId} using {_control.GetControlType()}";
    }
}
