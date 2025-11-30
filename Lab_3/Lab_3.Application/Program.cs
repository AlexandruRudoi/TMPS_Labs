using Lab_3.Application.DTOs;
using Lab_3.Application.Readers;
using Lab_3.Domain.Entities;
using Lab_3.Domain.Entities.Vehicles;
using Lab_3.Domain.Enums;
using Lab_3.Domain.Interfaces;
using Lab_3.Services;
using Lab_3.Services.Adapters;
using Lab_3.Services.Bridge;
using Lab_3.Services.Builders;
using Lab_3.Services.Composite;
using Lab_3.Services.Decorators;
using Lab_3.Services.Facade;
using Lab_3.Services.Factories;
using Lab_3.Services.Flyweight;
using Lab_3.Services.Pools;
using Lab_3.Services.Prototypes;
using Lab_3.Services.Proxy;

namespace Lab_3.Application;

/// <summary>
///     Smart Logistics Management System
///     Demonstrates advanced design patterns in a real-world logistics application
/// </summary>
internal class Program
{
    private static TrackingConfigurationDto? _configuration;
    private static readonly JsonConfigurationReader ConfigReader = new();

    private static void Main(string[] args)
    {
        PrintHeader();
        LoadConfiguration();

        try
        {
            RunLogisticsApplication();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("\n\nPress any key to exit...");
        Console.ReadKey();
    }

    private static void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                                                                ║");
        Console.WriteLine("║           SMART LOGISTICS MANAGEMENT SYSTEM                    ║");
        Console.WriteLine("║                                                                ║");
        Console.WriteLine("║         Lab 3: Behavioral Design Patterns                      ║");
        Console.WriteLine("║         Author: Alexandru Rudoi                                ║");
        Console.WriteLine("║                                                                ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    private static void LoadConfiguration()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Loading system configuration... ");

        try
        {
            _configuration = ConfigReader.Load<TrackingConfigurationDto>("tracking-systems.json");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done");
            Console.ResetColor();
            Console.WriteLine($"   Loaded {_configuration.TrackingSystems.Count} tracking systems");
            Console.WriteLine($"   Loaded {_configuration.Shipments.Count} shipment configurations\n");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed");
            Console.WriteLine($"   Error: {ex.Message}");
            Console.ResetColor();
            throw;
        }
    }

    private static void RunLogisticsApplication()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("════════════════════════════════════════════════════════════════");
        Console.WriteLine("                    MAIN MENU                                   ");
        Console.WriteLine("════════════════════════════════════════════════════════════════");
        Console.ResetColor();
        Console.WriteLine();

        Console.WriteLine();

        // STRUCTURAL PATTERNS - Demonstrating all patterns
        Console.WriteLine("STRUCTURAL DESIGN PATTERNS DEMONSTRATIONS:\n");

        // 1. ADAPTER PATTERN - Third-party system integration
        DemonstrateAdapter();

        // 2. DECORATOR PATTERN - Dynamic feature enhancement
        DemonstrateDecorator();

        // 3. COMPOSITE PATTERN - Hierarchical package grouping
        DemonstrateComposite();

        // 4. FACADE PATTERN - Simplified operations
        DemonstrateFacade();

        // 5. BRIDGE PATTERN - Decouple abstraction from implementation
        DemonstrateBridge();

        // 6. FLYWEIGHT PATTERN - Share common data efficiently
        DemonstrateFlyweight();

        // 7. PROXY PATTERN - Control access and lazy loading
        DemonstrateProxy();

        // INTEGRATED DEMO - All structural patterns working together
        DemonstrateIntegratedScenario();
    }

    private static void DemonstrateAdapter()
    {
        PrintSectionHeader("ADAPTER PATTERN", "Third-Party Tracking Systems Integration");
        Console.WriteLine("Purpose: Convert incompatible interfaces to work with our system\n");

        if (_configuration?.TrackingSystems == null || !_configuration.TrackingSystems.Any())
        {
            Console.WriteLine("Warning: No tracking systems configured\n");
            return;
        }

        // Create different tracking systems (adaptees with incompatible interfaces)
        ITrackingSystem[] trackingSystems =
        {
            new GpsTrackingAdapter(),
            new RfidTrackingAdapter(),
            new BarcodeTrackingAdapter()
        };

        var shipmentId = _configuration.TrackingSystems.First().ShipmentId;

        foreach (var system in trackingSystems)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"┌─ {system.SystemName} ───");
            Console.ResetColor();

            // Unified interface despite different underlying systems
            var location = system.Track(shipmentId);
            Console.WriteLine($"│ Current Location: {location}");

            Console.WriteLine("│ Tracking History:");
            var history = system.GetTrackingHistory(shipmentId);
            foreach (var entry in history.Take(3)) Console.WriteLine($"│    {entry}");

            var eta = system.GetEstimatedDelivery(shipmentId);
            Console.WriteLine($"│ Estimated Delivery: {eta:yyyy-MM-dd HH:mm}");
            Console.WriteLine("└────────────────────────────────────────\n");
        }

        PrintBenefits(
            "Adapter pattern enables unified interface for different systems",
            "Legacy systems integrated without modifying their code",
            "GPS (coordinates), RFID (dictionary), Barcode (XML) all work seamlessly"
        );
    }

    private static void DemonstrateDecorator()
    {
        PrintSectionHeader("DECORATOR PATTERN", "Dynamic Shipment Enhancements");
        Console.WriteLine("Purpose: Add responsibilities to objects dynamically without inheritance\n");

        // Create a base shipment (using creational patterns for object creation)
        var packages = new List<Package>
        {
            new("PKG-DEC-001", "Laptop Computer", 3m, "Tech Store", "Urban"),
            new("PKG-DEC-002", "Monitor", 5m, "Tech Store", "Urban")
        };

        var vehicle = new DeliveryTruck("V-DEC-001", "DEC-TRUCK-1", 1500m, "Urban");
        var driver = new Driver("D-DEC-001", "Mike Wilson", "Urban", DriverLicenseType.Delivery);
        var route = new Route("R-DEC-001", "Tech Delivery Route", "Urban")
        {
            TotalDistance = 15m,
            EstimatedDuration = TimeSpan.FromHours(1)
        };

        var builder = new ShipmentBuilder();
        var shipment = builder
            .SetId("SHP-DEC-001")
            .AddPackages(packages)
            .AssignVehicle(vehicle)
            .AssignDriver(driver)
            .SetRoute(route)
            .SetScheduledDate(DateTime.Now.AddHours(2))
            .Build();

        // Wrap in component and apply decorators (STRUCTURAL PATTERN DEMONSTRATION)
        Console.WriteLine("--- Building Enhanced Shipment with Decorators ---\n");

        IShipmentComponent component = new BasicShipmentComponent(shipment);
        Console.WriteLine($"1. Base: {component.GetDescription()}");
        Console.WriteLine($"   Cost: ${component.CalculateCost():F2}\n");

        component = new InsuranceDecorator(component, 2000m);
        Console.WriteLine($"2. + Insurance: {component.GetDescription()}");
        Console.WriteLine($"   Cost: ${component.CalculateCost():F2}\n");

        component = new PriorityDecorator(component);
        Console.WriteLine($"3. + Priority: {component.GetDescription()}");
        Console.WriteLine($"   Cost: ${component.CalculateCost():F2}\n");

        component = new FragileHandlingDecorator(component, "Fragile electronics - use bubble wrap");
        Console.WriteLine($"4. + Fragile: {component.GetDescription()}");
        Console.WriteLine($"   Cost: ${component.CalculateCost():F2}\n");

        component = new SignatureConfirmationDecorator(component, true);
        Console.WriteLine($"5. Final: {component.GetDescription()}");
        Console.WriteLine($"   Cost: ${component.CalculateCost():F2}\n");

        Console.WriteLine("--- Processing Enhanced Shipment ---");
        component.Process();

        Console.WriteLine("\nBenefits:");
        Console.WriteLine("- Decorator pattern adds features dynamically");
        Console.WriteLine("- Each decorator wraps the previous one");
        Console.WriteLine("- Features can be combined in any order");
        Console.WriteLine();
    }

    private static void DemonstrateComposite()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 3: COMPOSITE - Hierarchical Package Grouping");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Treat individual packages and groups uniformly\n");

        // Create individual packages
        var packages = new List<Package>
        {
            new("PKG-001", "Smartphone", 0.5m, "Electronics Store", "Urban"),
            new("PKG-002", "Tablet", 0.7m, "Electronics Store", "Urban"),
            new("PKG-003", "Headphones", 0.3m, "Electronics Store", "Urban"),
            new("PKG-004", "Smartwatch", 0.2m, "Electronics Store", "Urban"),
            new("PKG-005", "Laptop", 2.5m, "Electronics Store", "Urban"),
            new("PKG-006", "Camera", 1.2m, "Electronics Store", "Urban"),
            new("PKG-007", "Keyboard", 0.8m, "Electronics Store", "Urban"),
            new("PKG-008", "Mouse", 0.15m, "Electronics Store", "Urban")
        };

        Console.WriteLine("--- Simple Container Hierarchy ---");
        var box = PackageCompositeBuilder.CreateBox("BOX-001", packages.Take(4).ToList());
        box.Display();

        Console.WriteLine("\n--- Pallet with Multiple Boxes ---");
        var pallet = PackageCompositeBuilder.CreatePallet("PALLET-001", packages);
        pallet.Display();

        Console.WriteLine("\n--- Complex Multi-Level Hierarchy ---");
        var complexHierarchy = PackageCompositeBuilder.CreateComplexHierarchy("MASTER-PALLET-001", packages);
        complexHierarchy.Display();

        Console.WriteLine("\n--- Working with Composite Uniformly ---");
        Console.WriteLine($"Pallet Total Weight: {pallet.GetTotalWeight()}kg");
        Console.WriteLine($"Pallet Package Count: {pallet.GetPackageCount()} packages");
        Console.WriteLine($"Complex Hierarchy Total Weight: {complexHierarchy.GetTotalWeight()}kg");
        Console.WriteLine($"Complex Hierarchy Package Count: {complexHierarchy.GetPackageCount()} packages");

        // Flatten hierarchy
        var allPackages = complexHierarchy.GetAllPackages();
        Console.WriteLine($"\nFlattened packages from hierarchy: {allPackages.Count}");

        Console.WriteLine("\nBenefits:");
        Console.WriteLine("- Composite pattern enables tree structures");
        Console.WriteLine("- Individual packages and containers treated uniformly");
        Console.WriteLine("- Operations work on both leaves and composites");
        Console.WriteLine();
    }

    private static void DemonstrateFacade()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 4: FACADE - Simplified Logistics Operations");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Provide simple interface to complex subsystem\n");

        // Create facade (uses creational patterns internally)
        var facade = new LogisticsFacade();

        // Display system status
        facade.DisplaySystemStatus();

        // Create simple shipment with one method call
        Console.WriteLine("--- Creating Simple Shipment via Facade ---");
        var packages = new List<Package>
        {
            new("PKG-FAC-001", "Books", 5m, "Library", "Urban"),
            new("PKG-FAC-002", "Documents", 1m, "Office", "Urban")
        };

        var shipment = facade.CreateSimpleShipment(packages);
        Console.WriteLine($"Shipment created: {shipment.GetShipmentInfo()}\n");

        // Create premium shipment with enhancements (combines multiple structural patterns)
        Console.WriteLine("--- Creating Premium Shipment via Facade ---");
        var premiumPackages = new List<Package>
        {
            new("PKG-PREM-001", "Pharmaceutical Samples", 2m, "Hospital", "Urban")
                { RequiresRefrigeration = true },
            new("PKG-PREM-002", "Medical Equipment", 8m, "Clinic", "Urban")
        };

        var premiumShipment = facade.CreatePremiumShipment(
            premiumPackages,
            5000m,
            "Urgent",
            true
        );

        Console.WriteLine("\n--- Processing Premium Shipment ---");
        premiumShipment.Process();

        // Complete workflow with tracking (combines Facade with Adapter pattern)
        Console.WriteLine("\n--- Complete Shipment Workflow via Facade ---");
        var workflowPackages = new List<Package>
        {
            new("PKG-WF-001", "Electronics", 3m, "Tech Store", "Urban")
        };

        var trackingSystem = new GpsTrackingAdapter();
        facade.ProcessCompleteShipment(workflowPackages, trackingSystem);

        Console.WriteLine("\nBenefits:");
        Console.WriteLine("- Facade simplifies complex operations");
        Console.WriteLine("- Hides subsystem complexity from client");
        Console.WriteLine("- Coordinates factories, pools, builders, decorators, adapters");
        Console.WriteLine();
    }

    private static void DemonstrateBridge()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 5: BRIDGE - Decouple Abstraction from Implementation");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Separate vehicle operations from control system implementations\n");

        // Create different control systems (implementations)
        IVehicleControl manualControl = new ManualControl();
        IVehicleControl automaticControl = new AutomaticControl();
        IVehicleControl autonomousControl = new AutonomousControl();
        IVehicleControl remoteControl = new RemoteControl("OP-042");

        var vehicleId = "V-BRIDGE-001";

        Console.WriteLine("--- Same Vehicle with Different Control Systems ---\n");

        // Same vehicle operation, different implementations
        Console.WriteLine("1. Manual Control:");
        VehicleOperation startManual = new StartVehicleOperation(manualControl, vehicleId);
        VehicleOperation driveManual = new DriveVehicleOperation(manualControl, vehicleId);
        startManual.Execute();
        driveManual.Execute();

        Console.WriteLine("\n2. Automatic Control:");
        VehicleOperation startAuto = new StartVehicleOperation(automaticControl, vehicleId);
        VehicleOperation driveAuto = new DriveVehicleOperation(automaticControl, vehicleId);
        startAuto.Execute();
        driveAuto.Execute();

        Console.WriteLine("\n3. Autonomous Control:");
        VehicleOperation startAutonomous = new StartVehicleOperation(autonomousControl, vehicleId);
        VehicleOperation driveAutonomous = new DriveVehicleOperation(autonomousControl, vehicleId);
        VehicleOperation stopAutonomous = new StopVehicleOperation(autonomousControl, vehicleId);
        startAutonomous.Execute();
        driveAutonomous.Execute();
        stopAutonomous.Execute();

        Console.WriteLine("\n4. Remote Control:");
        VehicleOperation startRemote = new StartVehicleOperation(remoteControl, "DRONE-001");
        VehicleOperation driveRemote = new DriveVehicleOperation(remoteControl, "DRONE-001");
        startRemote.Execute();
        driveRemote.Execute();

        Console.WriteLine("\nBenefits:");
        Console.WriteLine("- Bridge pattern separates abstraction from implementation");
        Console.WriteLine("- Can mix and match operations with control systems");
        Console.WriteLine("- Easy to add new operations or control systems independently");
        Console.WriteLine();
    }

    private static void DemonstrateFlyweight()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 6: FLYWEIGHT - Share Common Data Efficiently");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Reduce memory usage by sharing common package type data\n");

        var factory = new PackageTypeFactory();

        Console.WriteLine("--- Creating Packages (Watch Flyweight Sharing) ---\n");

        // Create many packages - notice how flyweights are reused
        var packages = new List<FlyweightPackage>();

        Console.WriteLine("Creating Electronics packages:");
        packages.Add(new FlyweightPackage("PKG-001", 2.5m, "TechStore", "Customer A", "TRACK-001",
            factory.GetPackageType("electronics")));
        packages.Add(new FlyweightPackage("PKG-002", 1.8m, "TechStore", "Customer B", "TRACK-002",
            factory.GetPackageType("electronics")));
        packages.Add(new FlyweightPackage("PKG-003", 3.2m, "TechStore", "Customer C", "TRACK-003",
            factory.GetPackageType("electronics")));

        Console.WriteLine("\nCreating Books packages:");
        packages.Add(new FlyweightPackage("PKG-004", 5.0m, "Bookstore", "Library", "TRACK-004",
            factory.GetPackageType("books")));
        packages.Add(new FlyweightPackage("PKG-005", 4.5m, "Bookstore", "School", "TRACK-005",
            factory.GetPackageType("books")));

        Console.WriteLine("\nCreating Pharmaceuticals packages:");
        packages.Add(new FlyweightPackage("PKG-006", 1.2m, "MedSupply", "Hospital", "TRACK-006",
            factory.GetPackageType("pharmaceuticals")));
        packages.Add(new FlyweightPackage("PKG-007", 0.8m, "MedSupply", "Clinic", "TRACK-007",
            factory.GetPackageType("pharmaceuticals")));

        Console.WriteLine("\nCreating more Electronics (reusing flyweight):");
        packages.Add(new FlyweightPackage("PKG-008", 2.1m, "TechStore", "Customer D", "TRACK-008",
            factory.GetPackageType("electronics")));

        Console.WriteLine("\n--- Flyweight Statistics ---");
        Console.WriteLine($"Total Packages Created: {packages.Count}");
        Console.WriteLine($"Unique PackageType Flyweights: {factory.GetFlyweightCount()}");
        Console.WriteLine($"Memory Saved: {packages.Count - factory.GetFlyweightCount()} type definitions");
        Console.WriteLine($"\nCategories: {string.Join(", ", factory.GetCategories())}");

        Console.WriteLine("\n--- Sample Package Display ---");
        packages[0].Display();

        Console.WriteLine("\nBenefits:");
        Console.WriteLine("- Flyweight pattern shares intrinsic state across objects");
        Console.WriteLine("- Dramatically reduces memory usage for large datasets");
        Console.WriteLine($"- {packages.Count} packages share only {factory.GetFlyweightCount()} type objects");
        Console.WriteLine();
    }

    private static void DemonstrateProxy()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("PATTERN 7: PROXY - Control Access and Lazy Loading");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Purpose: Control access to objects and defer expensive operations\n");

        var shipmentId = "SHP-PROXY-001";

        // 1. Lazy Loading Proxy
        Console.WriteLine("--- 1. LAZY LOADING PROXY ---");
        Console.WriteLine("Delays expensive report loading until needed\n");

        IShipmentReport lazyReport = new LazyShipmentReportProxy(shipmentId);
        Console.WriteLine("Proxy created - report not loaded yet");

        Console.WriteLine("\nGetting summary (no loading needed):");
        Console.WriteLine($"   {lazyReport.GetSummary()}");

        Console.WriteLine("\nDisplaying report (triggers loading):");
        lazyReport.Display();

        // 2. Protection Proxy
        Console.WriteLine("\n--- 2. PROTECTION PROXY ---");
        Console.WriteLine("Controls access based on user permissions\n");

        Console.WriteLine("Scenario A: Regular user (no access):");
        IShipmentReport protectedReport1 = new ProtectedShipmentReportProxy(shipmentId, "user123", "Employee");
        protectedReport1.Display();

        Console.WriteLine("Scenario B: Manager (read access):");
        IShipmentReport protectedReport2 = new ProtectedShipmentReportProxy(shipmentId, "mgr456", "Manager");
        protectedReport2.Display();

        Console.WriteLine("Scenario C: Admin trying to export:");
        IShipmentReport protectedReport3 = new ProtectedShipmentReportProxy(shipmentId, "admin789", "Admin");
        Console.WriteLine($"\n   {protectedReport3.GetSummary()}");
        protectedReport3.ExportToPdf();

        Console.WriteLine("\nScenario D: Manager trying to export (denied):");
        protectedReport2.ExportToPdf();

        // 3. Caching Proxy
        Console.WriteLine("\n--- 3. CACHING PROXY ---");
        Console.WriteLine("Caches results to avoid repeated expensive operations\n");

        IShipmentReport cachingReport = new CachingShipmentReportProxy("SHP-CACHE-001");

        Console.WriteLine("First PDF export (cache miss):");
        cachingReport.ExportToPdf();

        Console.WriteLine("\nSecond PDF export (cache hit):");
        cachingReport.ExportToPdf();

        Console.WriteLine("\nThird PDF export (still cached):");
        cachingReport.ExportToPdf();

        Console.WriteLine("\nBenefits:");
        Console.WriteLine("- Proxy pattern provides controlled access to objects");
        Console.WriteLine("- Lazy proxy defers expensive operations until needed");
        Console.WriteLine("- Protection proxy enforces access control policies");
        Console.WriteLine("- Caching proxy improves performance through caching");
        Console.WriteLine();
    }

    private static void DemonstrateIntegratedScenario()
    {
        Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
        Console.WriteLine("INTEGRATED SCENARIO - All Structural Patterns Working Together");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("Demonstrating how all 7 structural patterns work together seamlessly\n");

        // Setup - using creational patterns for object creation only
        var vehicleFactory = new VehicleFactory("International");
        var vehicles = new List<Vehicle>
        {
            vehicleFactory.CreateDeliveryTruck("V401", "INT-DT-1", 1500m, true),
            vehicleFactory.CreateCargoPlane("V402", "INT-CP-2", 50000m, "Airbus A330F")
        };
        var vehiclePool = new VehiclePool(vehicles);

        var routeManager = new RoutePrototypeManager();
        var route = routeManager.CloneTemplateWithModifications(
            "urban-template", "ROUTE-INT-001", "Integrated Demo Route");

        var factory = new UrbanLogisticsFactory();
        var drivers = new List<Driver>
            { factory.CreateDriver("D401", "Captain Alice Johnson", DriverLicenseType.Aviation) };
        var driverPool = new DriverPool(drivers);

        // COMPOSITE PATTERN: Create hierarchical package structure
        Console.WriteLine("--- 1. COMPOSITE: Hierarchical Package Grouping ---");
        var packages = new List<Package>
        {
            new("PKG-INT-001", "Medical Supplies", 25m, "Hospital A", "Urban") { RequiresRefrigeration = true },
            new("PKG-INT-002", "Lab Equipment", 45m, "Research Center", "Urban"),
            new("PKG-INT-003", "Documents", 2m, "City Hall", "Urban"),
            new("PKG-INT-004", "Vaccines", 5m, "Clinic B", "Urban") { RequiresRefrigeration = true }
        };

        var packageHierarchy = PackageCompositeBuilder.CreatePallet("PALLET-INT-001", packages, 2);
        Console.WriteLine(
            $"Created hierarchy: {packageHierarchy.GetTotalWeight()}kg, {packageHierarchy.GetPackageCount()} packages\n");

        // Build shipment using builder (infrastructure)
        var vehicle = vehiclePool.Acquire();
        var driver = driverPool.Acquire();
        var builder = new ShipmentBuilder();
        var shipment = builder
            .SetId("SHP-INTEGRATED-001")
            .AddPackages(packageHierarchy.GetAllPackages())
            .AssignVehicle(vehicle)
            .AssignDriver(driver)
            .SetRoute(route)
            .SetScheduledDate(DateTime.Now.AddHours(4))
            .AddNotes("Priority delivery - medical supplies on board")
            .Build();

        // DECORATOR PATTERN: Enhance shipment dynamically
        Console.WriteLine("--- 2. DECORATOR: Dynamic Shipment Enhancements ---");
        IShipmentComponent enhancedShipment = new BasicShipmentComponent(shipment);
        enhancedShipment = new InsuranceDecorator(enhancedShipment, 10000m);
        enhancedShipment = new PriorityDecorator(enhancedShipment, "Urgent");
        enhancedShipment = new TemperatureMonitoringDecorator(enhancedShipment);
        enhancedShipment = new SignatureConfirmationDecorator(enhancedShipment, true);
        Console.WriteLine($"{enhancedShipment.GetDescription()}");
        Console.WriteLine($"Total Cost: ${enhancedShipment.CalculateCost():F2}\n");

        // ADAPTER PATTERN: Integrate tracking system
        Console.WriteLine("--- 3. ADAPTER: Third-Party Tracking Integration ---");
        ITrackingSystem tracking = new RfidTrackingAdapter();
        Console.WriteLine($"Tracking via {tracking.SystemName}");
        Console.WriteLine($"{tracking.Track(shipment.Id)}\n");

        // FACADE PATTERN: Validate using simplified interface
        Console.WriteLine("--- 4. FACADE: Simplified Validation ---");
        var service = new ShipmentService();
        if (service.ValidateShipment(shipment)) Console.WriteLine("Shipment validation PASSED\n");

        // BRIDGE PATTERN: Control vehicle with different implementations
        Console.WriteLine("--- 5. BRIDGE: Vehicle Control Abstraction ---");
        var autonomousControl = new AutonomousControl();
        var vehicleOp = new DriveVehicleOperation(autonomousControl, vehicle.Id);
        vehicleOp.Execute();
        Console.WriteLine();

        // FLYWEIGHT PATTERN: Efficient package type management
        Console.WriteLine("--- 6. FLYWEIGHT: Shared Package Type Data ---");
        var typeFactory = new PackageTypeFactory();
        var electronicsType = typeFactory.GetPackageType("electronics");
        var pharmaType = typeFactory.GetPackageType("pharmaceuticals");
        Console.WriteLine($"Reusing package types: {electronicsType.Category}, {pharmaType.Category}");
        Console.WriteLine($"Flyweights created: {typeFactory.GetFlyweightCount()}\n");

        // PROXY PATTERN: Controlled report access
        Console.WriteLine("--- 7. PROXY: Lazy Loading and Access Control ---");
        IShipmentReport report = new LazyShipmentReportProxy(shipment.Id);
        Console.WriteLine($"{report.GetSummary()}");
        Console.WriteLine();

        // Cleanup
        vehiclePool.Release(vehicle);
        driverPool.Release(driver);

        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("ALL 7 STRUCTURAL PATTERNS SUCCESSFULLY INTEGRATED:");
        Console.WriteLine("\nSTRUCTURAL PATTERNS:");
        Console.WriteLine("  1. Adapter - Third-party tracking system integration");
        Console.WriteLine("  2. Decorator - Dynamic shipment enhancements");
        Console.WriteLine("  3. Composite - Hierarchical package grouping");
        Console.WriteLine("  4. Facade - Simplified operations");
        Console.WriteLine("  5. Bridge - Decouple abstraction from implementation");
        Console.WriteLine("  6. Flyweight - Share common data efficiently");
        Console.WriteLine("  7. Proxy - Control access and lazy loading");
        Console.WriteLine("\nNote: Creational patterns (Singleton, Factory, Builder, Prototype, Pool)");
        Console.WriteLine("      used internally for object creation infrastructure.");
        Console.WriteLine("═══════════════════════════════════════════════════════════════\n");
    }

    // ═══════════════════════════════════════════════════════════════
    // Helper Methods
    // ═══════════════════════════════════════════════════════════════

    private static void PrintSectionHeader(string patternName, string description)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
        Console.WriteLine($"  {patternName}");
        Console.WriteLine($"  {description}");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.ResetColor();
    }

    private static void PrintBenefits(params string[] benefits)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Benefits:");
        Console.ResetColor();
        foreach (var benefit in benefits) Console.WriteLine($"  - {benefit}");
        Console.WriteLine();
    }
}