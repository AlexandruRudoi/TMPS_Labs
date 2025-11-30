using Lab_3.Domain.Entities;
using Lab_3.Domain.Entities.Vehicles;
using Lab_3.Domain.Enums;
using Lab_3.Domain.Interfaces;
using Lab_3.Services.Configuration;

namespace Lab_3.Services;

/// <summary>
///     Service for shipment management and validation
/// </summary>
public class ShipmentService : IShipmentService
{
    /// <summary>
    ///     Logistics configuration instance
    /// </summary>
    private readonly ILogisticsConfig _config;

    /// <summary>
    ///     Initializes a new instance of the ShipmentService
    /// </summary>
    public ShipmentService()
    {
        _config = LogisticsConfig.Instance;
    }

    /// <inheritdoc />
    public Shipment CreateShipment(IEnumerable<Package> packages, Vehicle vehicle, Driver driver, Route route)
    {
        var packageList = packages.ToList();

        var shipment = new Shipment
        {
            Id = GenerateShipmentId(),
            Packages = packageList,
            AssignedVehicle = vehicle,
            AssignedDriver = driver,
            Route = route,
            ScheduledDate = DateTime.Now.AddDays(1),
            Status = ShipmentStatus.Scheduled
        };

        shipment.CalculateTotalWeight();

        return shipment;
    }

    /// <inheritdoc />
    public bool ValidateShipment(Shipment shipment)
    {
        if (shipment == null)
            return false;

        if (shipment.Packages.Count > _config.MaxPackagesPerShipment)
        {
            Console.WriteLine($"ERROR: Shipment exceeds maximum package count ({_config.MaxPackagesPerShipment})");
            return false;
        }

        if (shipment.TotalWeight > shipment.AssignedVehicle.Capacity)
        {
            Console.WriteLine(
                $"ERROR: Total weight ({shipment.TotalWeight}kg) exceeds vehicle capacity ({shipment.AssignedVehicle.Capacity}kg)");
            return false;
        }

        if (!shipment.AssignedDriver.CanOperate(shipment.AssignedVehicle.Type))
        {
            Console.WriteLine(
                $"ERROR: Driver/Operator {shipment.AssignedDriver.Name} cannot operate {shipment.AssignedVehicle.Type}");
            return false;
        }

        if (shipment.AssignedVehicle.Region != shipment.AssignedDriver.Region)
            Console.WriteLine("WARNING: Vehicle and driver regions don't match");

        if (shipment.Route.TotalDistance > _config.MaxDailyDistance)
            Console.WriteLine(
                $"WARNING: Route distance ({shipment.Route.TotalDistance}km) exceeds daily limit ({_config.MaxDailyDistance}km)");

        return true;
    }

    /// <inheritdoc />
    public void DisplayShipmentDetails(Shipment shipment)
    {
        Console.WriteLine("\n--- Shipment Details ---");
        Console.WriteLine($"ID: {shipment.Id}");
        Console.WriteLine($"Status: {shipment.Status}");
        Console.WriteLine($"Scheduled: {shipment.ScheduledDate:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"Packages: {shipment.Packages.Count} ({shipment.TotalWeight}kg total)");
        Console.WriteLine($"\nVehicle: {shipment.AssignedVehicle.GetVehicleInfo()}");
        Console.WriteLine($"Driver: {shipment.AssignedDriver.Name} (License: {shipment.AssignedDriver.LicenseType})");
        Console.WriteLine($"\nRoute: {shipment.Route.GetRouteInfo()}");

        var fuelCost = shipment.AssignedVehicle.CalculateFuelCost(shipment.Route.TotalDistance);
        Console.WriteLine($"Estimated Fuel Cost: ${fuelCost:F2}");

        if (!string.IsNullOrEmpty(shipment.Notes))
            Console.WriteLine($"Notes: {shipment.Notes}");

        Console.WriteLine("------------------------\n");
    }

    /// <summary>
    ///     Generates a unique shipment identifier
    /// </summary>
    /// <returns>Formatted shipment ID</returns>
    private string GenerateShipmentId()
    {
        return $"SHP-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
    }
}