namespace Lab_3.Domain.Enums;

/// <summary>
///     Defines the types of licenses for vehicle operators
/// </summary>
public enum DriverLicenseType
{
    /// <summary>
    ///     Drone operator license
    /// </summary>
    Drone = 1,

    /// <summary>
    ///     Delivery truck license
    /// </summary>
    Delivery = 2,

    /// <summary>
    ///     Commercial cargo truck license
    /// </summary>
    Commercial = 3,

    /// <summary>
    ///     Ship captain and crew license for maritime transport
    /// </summary>
    Maritime = 4,

    /// <summary>
    ///     Pilot license for cargo planes
    /// </summary>
    Aviation = 5
}