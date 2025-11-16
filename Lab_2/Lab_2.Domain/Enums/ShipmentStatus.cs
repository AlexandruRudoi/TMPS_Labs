namespace Lab_2.Domain.Enums;

/// <summary>
///     Defines the status of a shipment
/// </summary>
public enum ShipmentStatus
{
    /// <summary>
    ///     Shipment is pending assignment
    /// </summary>
    Pending,
    
    /// <summary>
    ///     Shipment has been scheduled
    /// </summary>
    Scheduled,
    
    /// <summary>
    ///     Shipment is currently in transit
    /// </summary>
    InTransit,
    
    /// <summary>
    ///     Shipment has been delivered
    /// </summary>
    Delivered,
    
    /// <summary>
    ///     Shipment has been cancelled
    /// </summary>
    Cancelled
}
