namespace Lab_2.Domain.Enums;

/// <summary>
///     Defines the operational status of a vehicle
/// </summary>
public enum VehicleStatus
{
    /// <summary>
    ///     Vehicle is available for assignment
    /// </summary>
    Available,
    
    /// <summary>
    ///     Vehicle is currently in use on a delivery
    /// </summary>
    InUse,
    
    /// <summary>
    ///     Vehicle is undergoing maintenance
    /// </summary>
    Maintenance,
    
    /// <summary>
    ///     Vehicle is out of service
    /// </summary>
    OutOfService
}
