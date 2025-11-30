namespace Lab_3.Domain.Interfaces;

/// <summary>
///     Implementation interface for Bridge pattern
///     Defines how vehicles are controlled (different control systems)
/// </summary>
public interface IVehicleControl
{
    /// <summary>
    ///     Starts the vehicle
    /// </summary>
    string Start();

    /// <summary>
    ///     Stops the vehicle
    /// </summary>
    string Stop();

    /// <summary>
    ///     Accelerates the vehicle
    /// </summary>
    string Accelerate();

    /// <summary>
    ///     Applies brakes
    /// </summary>
    string Brake();

    /// <summary>
    ///     Gets control system type
    /// </summary>
    string GetControlType();
}