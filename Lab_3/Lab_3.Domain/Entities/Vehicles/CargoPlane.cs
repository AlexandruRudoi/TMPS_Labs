using Lab_3.Domain.Enums;

namespace Lab_3.Domain.Entities.Vehicles;

/// <summary>
///     Cargo Plane for air freight transport
/// </summary>
public class CargoPlane : Vehicle
{
    /// <summary>
    ///     Initializes a new instance of the CargoPlane class
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="registration">Aircraft registration number</param>
    /// <param name="capacity">Maximum cargo capacity in kg</param>
    /// <param name="region">Operational region</param>
    /// <param name="model">Aircraft model designation</param>
    public CargoPlane(string id, string registration, decimal capacity, string region, string model = "Boeing 747F")
        : base(id, registration, VehicleType.CargoPlane, capacity, region)
    {
        AircraftModel = model;
        MaxFlightRange = 8000m;
        PalletPositions = 30;
    }

    /// <summary>
    ///     Gets or sets the aircraft model designation
    /// </summary>
    public string AircraftModel { get; set; }

    /// <summary>
    ///     Gets or sets the maximum flight range in kilometers
    /// </summary>
    public decimal MaxFlightRange { get; set; }

    /// <summary>
    ///     Gets or sets the number of pallet loading positions
    /// </summary>
    public int PalletPositions { get; set; }

    /// <inheritdoc />
    public override decimal CalculateFuelCost(decimal distance)
    {
        return distance * 8.00m;
    }

    /// <inheritdoc />
    public override string GetVehicleInfo()
    {
        return
            $"Cargo Plane {LicensePlate} - Capacity: {Capacity}kg - Region: {Region} - Model: {AircraftModel} - Range: {MaxFlightRange}km - Pallets: {PalletPositions} - Status: {Status}";
    }
}