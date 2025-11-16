using Lab_2.Domain.Entities;
using Lab_2.Domain.Entities.Vehicles;
using Lab_2.Domain.Enums;
using Lab_2.Domain.Interfaces;
using Lab_2.Services.Builders;
using Lab_2.Services.Configuration;
using Lab_2.Services.Decorators;
using Lab_2.Services.Factories;
using Lab_2.Services.Pools;
using Lab_2.Services.Prototypes;

namespace Lab_2.Services.Facade;

/// <summary>
///     Facade Pattern - Provides simplified interface to complex logistics subsystem
///     Coordinates multiple subsystems: Factories, Pools, Builders, Decorators, Services
/// </summary>
public class LogisticsFacade
{
    // Subsystem components
    private readonly LogisticsConfig _config;
    private readonly VehicleFactory _vehicleFactory;
    private readonly RoutePrototypeManager _routeManager;
    private readonly ShipmentService _shipmentService;
    private VehiclePool _vehiclePool;
    private DriverPool _driverPool;

    /// <summary>
    ///     Initializes a new instance of the LogisticsFacade
    /// </summary>
    /// <param name="region">The operational region for logistics</param>
    public LogisticsFacade(string region = "Urban")
    {
        // Initialize all subsystems
        _config = LogisticsConfig.Instance;
        _vehicleFactory = new VehicleFactory(region);
        _routeManager = new RoutePrototypeManager();
        _shipmentService = new ShipmentService();

        InitializeResourcePools(region);
    }

    /// <summary>
    ///     Creates a simple shipment with minimal parameters
    ///     Handles all complexity internally
    /// </summary>
    /// <param name="packages">List of packages to ship</param>
    /// <param name="routeTemplate">Route template name (optional)</param>
    /// <param name="vehicleType">Preferred vehicle type (optional)</param>
    /// <returns>Created and validated shipment</returns>
    public Shipment CreateSimpleShipment(
        List<Package> packages,
        string routeTemplate = "urban-template",
        VehicleType? vehicleType = null)
    {
        Console.WriteLine("\n Facade: Creating simple shipment...");

        // 1. Clone route from prototype
        var route = _routeManager.CloneTemplateWithModifications(
            routeTemplate,
            $"ROUTE-{Guid.NewGuid().ToString().Substring(0, 8)}",
            "Auto-generated route"
        );

        // 2. Acquire vehicle from pool
        var vehicle = vehicleType.HasValue
            ? _vehiclePool.AcquireByType(vehicleType.Value)
            : _vehiclePool.Acquire();

        if (vehicle == null)
        {
            throw new InvalidOperationException("No vehicles available in pool");
        }

        // 3. Acquire driver from pool
        var driver = _driverPool.AcquireWithLicense(GetRequiredLicense(vehicle.Type));

        if (driver == null)
        {
            _vehiclePool.Release(vehicle);
            throw new InvalidOperationException($"No drivers available for {vehicle.Type}");
        }

        // 4. Build shipment
        var builder = new ShipmentBuilder();
        var shipment = builder
            .SetId($"SHP-FACADE-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}")
            .AddPackages(packages)
            .AssignVehicle(vehicle)
            .AssignDriver(driver)
            .SetRoute(route)
            .SetScheduledDate(DateTime.Now.AddDays(1))
            .Build();

        // 5. Validate shipment
        if (!_shipmentService.ValidateShipment(shipment))
        {
            _vehiclePool.Release(vehicle);
            _driverPool.Release(driver);
            throw new InvalidOperationException("Shipment validation failed");
        }

        Console.WriteLine($"Shipment created successfully: {shipment.Id}");
        return shipment;
    }

    /// <summary>
    ///     Creates a premium shipment with all enhancements
    ///     Applies decorators for insurance, priority, monitoring
    /// </summary>
    /// <param name="packages">List of packages to ship</param>
    /// <param name="insuranceValue">Insurance coverage value</param>
    /// <param name="priorityLevel">Priority level (Express, Urgent, Standard)</param>
    /// <param name="requiresTemperatureControl">Whether temperature monitoring is needed</param>
    /// <returns>Enhanced shipment component with all features</returns>
    public IShipmentComponent CreatePremiumShipment(
        List<Package> packages,
        decimal insuranceValue = 1000m,
        string priorityLevel = "Express",
        bool requiresTemperatureControl = false)
    {
        Console.WriteLine("\n Facade: Creating premium shipment with enhancements...");

        // Create base shipment
        var shipment = CreateSimpleShipment(packages);

        // Wrap in component
        IShipmentComponent component = new BasicShipmentComponent(shipment);

        // Apply decorators
        component = new InsuranceDecorator(component, insuranceValue);
        component = new PriorityDecorator(component, priorityLevel);

        if (requiresTemperatureControl)
        {
            component = new TemperatureMonitoringDecorator(component);
        }

        // Check if any package is fragile (simplified check)
        if (packages.Any(p => p.Description.Contains("fragile", StringComparison.OrdinalIgnoreCase) ||
                              p.Description.Contains("glass", StringComparison.OrdinalIgnoreCase)))
        {
            component = new FragileHandlingDecorator(component);
        }

        component = new SignatureConfirmationDecorator(component, requiresIdVerification: true);

        Console.WriteLine($"Premium shipment created: {component.GetDescription()}");
        Console.WriteLine($"Total cost: ${component.CalculateCost():F2}");

        return component;
    }

    /// <summary>
    ///     Processes a shipment from creation to completion
    ///     Handles all steps: create, validate, track, complete
    /// </summary>
    /// <param name="packages">List of packages to ship</param>
    /// <param name="trackingSystem">Tracking system to use (GPS, RFID, Barcode)</param>
    public void ProcessCompleteShipment(List<Package> packages, ITrackingSystem trackingSystem)
    {
        Console.WriteLine("\n Facade: Processing complete shipment workflow...");

        // Create shipment
        var shipment = CreateSimpleShipment(packages);

        // Display details
        _shipmentService.DisplayShipmentDetails(shipment);

        // Track shipment
        Console.WriteLine($"\n Tracking via {trackingSystem.SystemName}:");
        var location = trackingSystem.Track(shipment.Id);
        Console.WriteLine($"   {location}");

        var history = trackingSystem.GetTrackingHistory(shipment.Id);
        Console.WriteLine("\n   Tracking History:");
        foreach (var entry in history)
        {
            Console.WriteLine($"   {entry}");
        }

        var eta = trackingSystem.GetEstimatedDelivery(shipment.Id);
        Console.WriteLine($"\n   Estimated Delivery: {eta:yyyy-MM-dd HH:mm}");

        // Complete shipment
        shipment.Status = ShipmentStatus.Delivered;
        Console.WriteLine($"\nShipment {shipment.Id} delivered successfully!"); // Release resources
        ReleaseShipmentResources(shipment);
    }

    /// <summary>
    ///     Releases vehicle and driver back to pools
    /// </summary>
    /// <param name="shipment">The completed shipment</param>
    public void ReleaseShipmentResources(Shipment shipment)
    {
        if (shipment.AssignedVehicle != null)
        {
            _vehiclePool.Release(shipment.AssignedVehicle);
            Console.WriteLine($"Vehicle {shipment.AssignedVehicle.LicensePlate} released to pool");
        }

        if (shipment.AssignedDriver != null)
        {
            _driverPool.Release(shipment.AssignedDriver);
            Console.WriteLine($"Driver {shipment.AssignedDriver.Name} released to pool");
        }
    }

    /// <summary>
    ///     Creates a batch of shipments efficiently
    /// </summary>
    /// <param name="packageGroups">Groups of packages, each group becomes a shipment</param>
    /// <returns>List of created shipments</returns>
    public List<Shipment> CreateBatchShipments(List<List<Package>> packageGroups)
    {
        Console.WriteLine($"\n Facade: Creating batch of {packageGroups.Count} shipments...");

        var shipments = new List<Shipment>();

        foreach (var packages in packageGroups)
        {
            try
            {
                var shipment = CreateSimpleShipment(packages);
                shipments.Add(shipment);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WARNING: Failed to create shipment: {ex.Message}");
            }
        }

        Console.WriteLine($"Created {shipments.Count}/{packageGroups.Count} shipments successfully");
        return shipments;
    }

    /// <summary>
    ///     Gets system statistics and status
    /// </summary>
    public void DisplaySystemStatus()
    {
        Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
        Console.WriteLine(" LOGISTICS SYSTEM STATUS");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");

        Console.WriteLine("\n Configuration:");
        _config.DisplayConfiguration();

        Console.WriteLine("\n Vehicle Pool:");
        Console.WriteLine($"   Total Vehicles: {_vehiclePool.TotalCount}");
        Console.WriteLine($"   Available: {_vehiclePool.AvailableCount}");
        Console.WriteLine($"   In Use: {_vehiclePool.TotalCount - _vehiclePool.AvailableCount}");

        Console.WriteLine("\n Driver Pool:");
        Console.WriteLine($"   Total Drivers: {_driverPool.TotalCount}");
        Console.WriteLine($"   Available: {_driverPool.AvailableCount}");
        Console.WriteLine($"   In Use: {_driverPool.TotalCount - _driverPool.AvailableCount}");

        Console.WriteLine("\n Available Route Templates:");
        foreach (var template in _routeManager.GetAvailableTemplates())
        {
            Console.WriteLine($"   - {template}");
        }

        Console.WriteLine("═══════════════════════════════════════════════════════════════\n");
    }

    /// <summary>
    ///     Initializes vehicle and driver pools
    /// </summary>
    private void InitializeResourcePools(string region)
    {
        // Create initial vehicles
        var vehicles = new List<Vehicle>
        {
            _vehicleFactory.CreateDeliveryTruck("V-POOL-001", "DT-001", 1500m, true),
            _vehicleFactory.CreateDeliveryTruck("V-POOL-002", "DT-002", 1500m, false),
            _vehicleFactory.CreateCargoTruck("V-POOL-003", "CT-001", 8000m, 4),
            _vehicleFactory.CreateDrone("V-POOL-004", "DRN-001", 25m, true),
            _vehicleFactory.CreateCargoPlane("V-POOL-005", "CP-001", 50000m, "Boeing 777F")
        };

        _vehiclePool = new VehiclePool(vehicles);

        // Create initial drivers
        var drivers = new List<Driver>
        {
            new Driver("D-POOL-001", "John Smith", region, DriverLicenseType.Delivery),
            new Driver("D-POOL-002", "Jane Doe", region, DriverLicenseType.Commercial),
            new Driver("D-POOL-003", "Bob Johnson", region, DriverLicenseType.Drone),
            new Driver("D-POOL-004", "Alice Williams", region, DriverLicenseType.Aviation),
            new Driver("D-POOL-005", "Charlie Brown", region, DriverLicenseType.Maritime)
        };

        _driverPool = new DriverPool(drivers);
    }

    /// <summary>
    ///     Gets required license type for vehicle type
    /// </summary>
    private DriverLicenseType GetRequiredLicense(VehicleType vehicleType)
    {
        return vehicleType switch
        {
            VehicleType.Drone => DriverLicenseType.Drone,
            VehicleType.DeliveryTruck => DriverLicenseType.Delivery,
            VehicleType.CargoTruck => DriverLicenseType.Commercial,
            VehicleType.CargoShip or VehicleType.ContainerVessel => DriverLicenseType.Maritime,
            VehicleType.CargoPlane => DriverLicenseType.Aviation,
            _ => DriverLicenseType.Delivery
        };
    }
}