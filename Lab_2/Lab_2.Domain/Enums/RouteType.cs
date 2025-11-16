namespace Lab_2.Domain.Enums;

/// <summary>
///     Defines the types of delivery routes
/// </summary>
public enum RouteType
{
    /// <summary>
    ///     Standard route type
    /// </summary>
    Standard,
    
    /// <summary>
    ///     Express route for priority deliveries
    /// </summary>
    Express,
    
    /// <summary>
    ///     Rural route covering countryside areas
    /// </summary>
    Rural,
    
    /// <summary>
    ///     Urban route within city limits
    /// </summary>
    Urban
}
