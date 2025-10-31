namespace Lab_1.Domain.Enums;

/// <summary>
///     Defines the current status of a driver
/// </summary>
public enum DriverStatus
{
    /// <summary>
    ///     Driver is available for assignment
    /// </summary>
    Available,
    
    /// <summary>
    ///     Driver is currently on a route
    /// </summary>
    OnRoute,
    
    /// <summary>
    ///     Driver is on break
    /// </summary>
    OnBreak,
    
    /// <summary>
    ///     Driver is off duty
    /// </summary>
    OffDuty
}
