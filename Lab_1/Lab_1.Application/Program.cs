using Lab_1.Domain.Entities;
using Lab_1.Domain.Entities.Vehicles;
using Lab_1.Domain.Enums;
using Lab_1.Domain.Factory;
using Lab_1.Services;
using Lab_1.Services.Builders;
using Lab_1.Services.Configuration;
using Lab_1.Services.Factories;
using Lab_1.Services.Pools;
using Lab_1.Services.Prototypes;

namespace Lab_1.Application;

/// <summary>
///     Demonstrates all implemented Creational Design Patterns in a Logistics/Dispatch system
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║    Creational Design Patterns - Logistics System Demo          ║");
        Console.WriteLine("║    TMPS Lab 1 - Alexandru Rudoi                                ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════╝\n");

        // 1. SINGLETON PATTERN - Configuration
        DemonstrateSingleton();

        // 2. ABSTRACT FACTORY PATTERN - Region-specific logistics
        DemonstrateAbstractFactory();

        // 3. FACTORY METHOD PATTERN - Vehicle creation
        DemonstrateFactoryMethod();

        // 4. BUILDER PATTERN - Complex shipment creation
        DemonstrateBuilder();

        // 5. PROTOTYPE PATTERN - Route templates
        DemonstratePrototype();

        // 6. OBJECT POOL PATTERN - Resource management
        DemonstrateObjectPool();

        // 7. INTEGRATED DEMO - All patterns working together
        DemonstrateIntegratedScenario();

        Console.WriteLine("\n╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║  All Creational Design Patterns Successfully Demonstrated!     ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static void DemonstrateSingleton()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 1: SINGLETON - Centralized Configuration");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Ensure only one instance of configuration exists\n");

        var config1 = LogisticsConfig.Instance;
        var config2 = LogisticsConfig.Instance;

        Console.WriteLine($"config1 == config2: {ReferenceEquals(config1, config2)}");
        Console.WriteLine("Both references point to the SAME instance!\n");

        config1.DisplayConfiguration();

        Console.WriteLine("✓ Singleton pattern ensures single configuration instance");
        Console.WriteLine("✓ Thread-safe implementation using Lazy<T>");
        Console.WriteLine();
    }

    static void DemonstrateAbstractFactory()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 2: ABSTRACT FACTORY - Region-Specific Logistics");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Create families of related objects (vehicles/drivers) per region\n");

        // Create factories for different regions
        ILogisticsFactory[] factories =
        {
            new UrbanLogisticsFactory(),
            new RuralLogisticsFactory(),
            new InternationalLogisticsFactory()
        };

        foreach (var factory in factories)
        {
            Console.WriteLine($"--- {factory.GetRegion()} Region Factory ---");

            var standardVehicle = factory.CreateStandardVehicle("V001", $"{factory.GetRegion()}-001");
            var heavyVehicle = factory.CreateHeavyVehicle("V002", $"{factory.GetRegion()}-002");
            var lightVehicle = factory.CreateLightVehicle("V003", $"{factory.GetRegion()}-003");

            Console.WriteLine($"Standard: {standardVehicle.GetVehicleInfo()}");
            Console.WriteLine($"Heavy: {heavyVehicle.GetVehicleInfo()}");
            Console.WriteLine($"Light: {lightVehicle.GetVehicleInfo()}");
            Console.WriteLine($"Fuel Multiplier: {factory.GetRegionalFuelMultiplier()}x\n");
        }

        Console.WriteLine("✓ Abstract Factory creates region-specific vehicle families");
        Console.WriteLine("✓ Each factory produces compatible objects for its region");
        Console.WriteLine();
    }

    static void DemonstrateFactoryMethod()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 3: FACTORY METHOD - Simple Vehicle Creation");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Provide interface for creating objects without specifying exact class\n");

        var factory = new VehicleFactory("Urban");

        var deliveryTruck = factory.CreateDeliveryTruck("V101", "DT-ABC-123", 1500m, hasRefrigeration: true);
        var cargoTruck = factory.CreateCargoTruck("V102", "CT-XYZ-789", 8000m, axleCount: 4);
        var drone = factory.CreateDrone("V103", "DRN-456", 25m, isAutonomous: true);
        var cargoShip = factory.CreateCargoShip("V104", "SS Logistics", 100000m, containerCapacity: 200);
        var cargoPlane = factory.CreateCargoPlane("V105", "CP-777", 50000m, "Boeing 777F");

        Console.WriteLine($"Created: {deliveryTruck.GetVehicleInfo()}");
        Console.WriteLine($"Created: {cargoTruck.GetVehicleInfo()}");
        Console.WriteLine($"Created: {drone.GetVehicleInfo()}");
        Console.WriteLine($"Created: {cargoShip.GetVehicleInfo()}");
        Console.WriteLine($"Created: {cargoPlane.GetVehicleInfo()}");

        Console.WriteLine("\n✓ Factory Method encapsulates vehicle creation logic");
        Console.WriteLine("✓ Client code doesn't need to know concrete vehicle classes");
        Console.WriteLine();
    }

    static void DemonstrateBuilder()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 4: BUILDER - Complex Shipment Construction");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Construct complex objects step-by-step with validation\n");

        // Create components
        var packages = new List<Package>
        {
            new Package("PKG-001", "Electronics", 15m, "123 Main St", "Urban"),
            new Package("PKG-002", "Books", 8m, "456 Oak Ave", "Urban"),
            new Package("PKG-003", "Clothing", 5m, "789 Pine Rd", "Urban")
        };

        var vehicle = new DeliveryTruck("V200", "BLD-DT-100", 1500m, "Urban", false);
        var driver = new Driver("D200", "John Smith", "Urban", DriverLicenseType.Delivery);
        var route = new Route("R200", "Downtown Circuit", "Urban")
        {
            TotalDistance = 30m,
            EstimatedDuration = TimeSpan.FromHours(2),
            Waypoints = new List<string> { "Warehouse", "Main St", "Oak Ave", "Pine Rd" }
        };

        // Build shipment step-by-step
        var builder = new ShipmentBuilder();

        Console.WriteLine("Building shipment step-by-step:");
        var shipment = builder
            .SetId("SHP-DEMO-001")
            .AddPackages(packages)
            .AssignVehicle(vehicle)
            .AssignDriver(driver)
            .SetRoute(route)
            .SetScheduledDate(DateTime.Now.AddDays(1))
            .AddNotes("Handle with care - fragile items")
            .Build();

        Console.WriteLine($"✓ Shipment built: {shipment.GetShipmentInfo()}");
        Console.WriteLine($"  - {shipment.Packages.Count} packages totaling {shipment.TotalWeight}kg");
        Console.WriteLine($"  - Vehicle: {vehicle.Type} ({vehicle.LicensePlate})");
        Console.WriteLine($"  - Driver: {driver.Name}");
        Console.WriteLine($"  - Route: {route.Name} ({route.TotalDistance}km)");

        Console.WriteLine("\n✓ Builder pattern enables fluent, step-by-step construction");
        Console.WriteLine("✓ Validates requirements before creating final object");
        Console.WriteLine();
    }

    static void DemonstratePrototype()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 5: PROTOTYPE - Route Template Cloning");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Clone existing objects to create new instances efficiently\n");

        var prototypeManager = new RoutePrototypeManager();

        Console.WriteLine("Available route templates:");
        foreach (var template in prototypeManager.GetAvailableTemplates())
        {
            Console.WriteLine($"  - {template}");
        }

        Console.WriteLine();

        // Clone urban template
        var urbanRoute1 = prototypeManager.CloneTemplateWithModifications(
            "urban-template", "URBAN-101", "City Center Loop - Morning");

        var urbanRoute2 = prototypeManager.CloneTemplateWithModifications(
            "urban-template", "URBAN-102", "City Center Loop - Evening");

        Console.WriteLine("Cloned urban routes:");
        Console.WriteLine($"Route 1: {urbanRoute1.GetRouteInfo()}");
        Console.WriteLine($"Route 2: {urbanRoute2.GetRouteInfo()}");

        // Clone rural template
        var ruralRoute = prototypeManager.CloneTemplateWithModifications(
            "rural-template", "RURAL-201", "Countryside Circuit - Weekly");

        Console.WriteLine($"\nCloned rural route: {ruralRoute.GetRouteInfo()}");

        Console.WriteLine("\n✓ Prototype pattern enables efficient object cloning");
        Console.WriteLine("✓ Templates can be customized after cloning");
        Console.WriteLine("✓ Avoids expensive initialization for similar objects");
        Console.WriteLine();
    }

    static void DemonstrateObjectPool()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 6: OBJECT POOL - Resource Management");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Reuse expensive objects instead of creating/destroying\n");

        // Create vehicle pool
        var vehicles = new List<Vehicle>
        {
            new DeliveryTruck("V301", "POOL-DT-1", 1500m, "Urban", false),
            new CargoTruck("V302", "POOL-CT-2", 8000m, "Urban", 3),
            new Drone("V303", "POOL-DRN-3", 25m, "Urban", true),
            new CargoShip("V304", "Pool Ship Alpha", 100000m, "Urban", 150)
        };

        var vehiclePool = new VehiclePool(vehicles);

        Console.WriteLine($"Vehicle Pool initialized: {vehiclePool.TotalCount} vehicles");
        Console.WriteLine($"Available: {vehiclePool.AvailableCount}\n");

        // Acquire vehicles
        Console.WriteLine("Acquiring vehicles from pool:");
        var v1 = vehiclePool.Acquire();
        Console.WriteLine($"  ✓ Acquired: {v1.GetVehicleInfo()}");

        var v2 = vehiclePool.AcquireByType(VehicleType.Drone);
        Console.WriteLine($"  ✓ Acquired: {v2.GetVehicleInfo()}");

        Console.WriteLine(
            $"\nPool status - Available: {vehiclePool.AvailableCount}, In Use: {vehiclePool.TotalCount - vehiclePool.AvailableCount}");

        // Release vehicles
        Console.WriteLine("\nReleasing vehicles back to pool:");
        vehiclePool.Release(v1);
        Console.WriteLine($"  ✓ Released: {v1.LicensePlate}");

        vehiclePool.Release(v2);
        Console.WriteLine($"  ✓ Released: {v2.LicensePlate}");

        Console.WriteLine(
            $"\nPool status - Available: {vehiclePool.AvailableCount}, In Use: {vehiclePool.TotalCount - vehiclePool.AvailableCount}");

        Console.WriteLine("\n✓ Object Pool manages resource lifecycle efficiently");
        Console.WriteLine("✓ Thread-safe acquire/release operations");
        Console.WriteLine("✓ Reduces object creation overhead");
        Console.WriteLine();
    }

    static void DemonstrateIntegratedScenario()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 7: INTEGRATED SCENARIO - All Patterns Together");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Demonstrate how all patterns work together in real scenario\n");

        // 1. Get Singleton configuration
        var config = LogisticsConfig.Instance;
        Console.WriteLine("✓ Using Singleton configuration\n");

        // 2. Create regional factory (Abstract Factory)
        ILogisticsFactory factory = new UrbanLogisticsFactory();
        Console.WriteLine($"✓ Created {factory.GetRegion()} logistics factory\n");

        // 3. Create vehicles using factory
        var vehicleFactory = new VehicleFactory("International");
        var vehicles = new List<Vehicle>
        {
            vehicleFactory.CreateDeliveryTruck("V401", "INT-DT-1", 1500m, true),
            vehicleFactory.CreateCargoPlane("V402", "INT-CP-2", 50000m, "Airbus A330F"),
            vehicleFactory.CreateContainerVessel("V403", "MV Container King", 250000m, 1000)
        };
        Console.WriteLine($"✓ Created {vehicles.Count} vehicles using Factory Method\n");

        // 4. Initialize Object Pool
        var vehiclePool = new VehiclePool(vehicles);
        Console.WriteLine($"✓ Initialized Vehicle Pool with {vehiclePool.TotalCount} vehicles\n");

        // 5. Clone route from Prototype
        var routeManager = new RoutePrototypeManager();
        var route = routeManager.CloneTemplateWithModifications(
            "urban-template", "ROUTE-INT-001", "Integrated Demo Route");
        Console.WriteLine($"✓ Cloned route using Prototype: {route.Name}\n");

        // 6. Create drivers
        var drivers = new List<Driver>
        {
            factory.CreateDriver("D401", "Captain Alice Johnson", DriverLicenseType.Aviation)
        };
        var driverPool = new DriverPool(drivers);
        Console.WriteLine($"✓ Created driver pool with {driverPool.TotalCount} drivers\n");

        // 7. Build shipment using Builder
        var packages = new List<Package>
        {
            new Package("PKG-INT-001", "Medical Supplies", 25m, "Hospital A", "Urban") { RequiresRefrigeration = true },
            new Package("PKG-INT-002", "Lab Equipment", 45m, "Research Center", "Urban"),
            new Package("PKG-INT-003", "Documents", 2m, "City Hall", "Urban")
        };

        var vehicle = vehiclePool.Acquire();
        var driver = driverPool.Acquire();

        var builder = new ShipmentBuilder();
        var shipment = builder
            .SetId("SHP-INTEGRATED-001")
            .AddPackages(packages)
            .AssignVehicle(vehicle)
            .AssignDriver(driver)
            .SetRoute(route)
            .SetScheduledDate(DateTime.Now.AddHours(4))
            .AddNotes("Priority delivery - medical supplies on board")
            .Build();

        Console.WriteLine("✓ Built complex shipment using Builder pattern\n");

        // 8. Validate and display
        var service = new ShipmentService();

        Console.WriteLine("--- Final Shipment Validation ---");
        if (service.ValidateShipment(shipment))
        {
            Console.WriteLine("✓ Shipment validation PASSED\n");
            service.DisplayShipmentDetails(shipment);
        }

        // 9. Release resources back to pool
        vehiclePool.Release(vehicle);
        driverPool.Release(driver);
        Console.WriteLine("✓ Resources released back to pools\n");

        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("ALL 6 CREATIONAL PATTERNS SUCCESSFULLY INTEGRATED:");
        Console.WriteLine("  1. ✓ Singleton - Configuration management");
        Console.WriteLine("  2. ✓ Abstract Factory - Region-specific logistics");
        Console.WriteLine("  3. ✓ Factory Method - Vehicle creation");
        Console.WriteLine("  4. ✓ Builder - Complex shipment construction");
        Console.WriteLine("  5. ✓ Prototype - Route template cloning");
        Console.WriteLine("  6. ✓ Object Pool - Resource lifecycle management");
        Console.WriteLine("═══════════════════════════════════════════════════════════════\n");
    }
}