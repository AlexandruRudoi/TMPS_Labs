namespace Lab_1.Domain.Enums;

/// <summary>
///     Defines the types of vehicles available in the logistics system
/// </summary>
public enum VehicleType
{
    /// <summary>
    ///     Light delivery truck for local/regional transport
    /// </summary>
    DeliveryTruck,
    
    /// <summary>
    ///     Heavy cargo truck for long-haul ground transport
    /// </summary>
    CargoTruck,
    
    /// <summary>
    ///     Cargo ship for maritime bulk transport
    /// </summary>
    CargoShip,
    
    /// <summary>
    ///     Container vessel for large-scale maritime container transport
    /// </summary>
    ContainerVessel,
    
    /// <summary>
    ///     Cargo plane for air freight transport
    /// </summary>
    CargoPlane,
    
    /// <summary>
    ///     Drone for last-mile delivery and urgent small packages
    /// </summary>
    Drone
}
