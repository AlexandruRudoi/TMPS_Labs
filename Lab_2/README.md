# **Structural Design Patterns Implementation in Logistics System**

### **Course**: Software Design Techniques and Mechanisms

### **Author**: Alexandru Rudoi

---

## **Theory**

This project demonstrates **7 Structural Design Patterns** in a logistics/dispatch system using Clean Architecture. Structural patterns explain how to assemble objects and classes into larger structures while keeping these structures flexible and efficient.

**Note**: This system also includes 6 creational patterns (Singleton, Abstract Factory, Factory Method, Builder, Prototype, Object Pool) from Lab 1 as the foundation for object creation, but this lab focuses specifically on structural patterns.

### **Structural Design Patterns (7)**

**Structural Design Patterns** focus on simplifying relationships between entities, composing objects into larger structures, and ensuring flexibility in how classes and objects are combined:

### **1. Adapter Pattern**
Allows incompatible interfaces to work together by **wrapping an existing class with a new interface**. Enables integration of third-party systems without modifying their code.

### **2. Decorator Pattern**
Attaches **additional responsibilities to objects dynamically** without altering their structure. Provides a flexible alternative to subclassing for extending functionality.

### **3. Composite Pattern**
Composes objects into **tree structures to represent part-whole hierarchies**. Allows clients to treat individual objects and compositions uniformly.

### **4. Facade Pattern**
Provides a **simplified interface to a complex subsystem**. Reduces dependencies and makes the subsystem easier to use.

### **5. Bridge Pattern**
Separates an **abstraction from its implementation** so they can vary independently. Useful when both abstractions and implementations need to be extended.

### **6. Flyweight Pattern**
Uses sharing to support **large numbers of fine-grained objects efficiently**. Reduces memory consumption by sharing common state between objects.

### **7. Proxy Pattern**
Provides a **surrogate or placeholder for another object** to control access. Useful for lazy initialization, access control, logging, or caching.

---

The implementation uses **Clean Architecture** with clear separation of concerns across multiple layers, demonstrating how structural patterns create flexible, maintainable, and efficient software systems by managing object relationships and compositions effectively.

---

## **Objectives**

- Implement **Clean Architecture** with:
  - **Domain Layer**: Core entities, interfaces, and enums
  - **Services Layer**: Pattern implementations and business logic  
  - **Application Layer**: Pattern demonstrations and orchestration

- Demonstrate **7 Structural Patterns** (Lab 2 Focus):
  - **Adapter**: Unifying third-party tracking systems (GPS, RFID, Barcode)
  - **Decorator**: Adding dynamic features to shipments (insurance, priority, temperature control, etc.)
  - **Composite**: Managing hierarchical package groupings (containers and packages)
  - **Facade**: Simplifying complex logistics operations through unified interface
  - **Bridge**: Separating vehicle operations from control systems implementations
  - **Flyweight**: Sharing package type data across many packages to optimize memory
  - **Proxy**: Controlling access to shipment reports (lazy loading, protection, caching)

- Foundation: **6 Creational Patterns** (from Lab 1):
  - Singleton, Abstract Factory, Factory Method, Builder, Prototype, Object Pool provide the object creation infrastructure

---

## **Implementation Description**

My logistics/dispatch system implements **7 structural design patterns** (Lab 2) built on top of a foundation of 6 creational patterns (Lab 1). The system uses three architectural layers:

### **1️⃣ Domain Layer (`Lab_2.Domain`)**

Contains the core business entities, contracts, and type definitions:

#### **Entities:**
- **`Vehicle`** (abstract): Base class for all transport vehicles
- **`DeliveryTruck`**, **`CargoTruck`**, **`CargoShip`**, **`ContainerVessel`**, **`CargoPlane`**, **`Drone`**: Concrete vehicles
- **`Driver`**, **`Package`**, **`Route`**, **`Shipment`**: Supporting entities

#### **Interfaces for Structural Patterns:**
- **`ITrackingSystem`**: Adapter pattern - unified tracking interface
- **`IShipmentComponent`**: Decorator pattern - dynamic enhancements
- **`IPackageComponent`**: Composite pattern - hierarchical structures
- **`IVehicleControl`**: Bridge pattern - control system interface
- **`IShipmentReport`**: Proxy pattern - report access control

#### **Interfaces for Creational Patterns (Foundation):**
- `ILogisticsFactory`, `IVehicleFactory`, `IShipmentBuilder`, `IRoutePrototype`, `IResourcePool<T>`, `ILogisticsConfig`

#### **Enums:**
Seven enums in separate files: `VehicleType`, `VehicleStatus`, `DriverLicenseType`, `DriverStatus`, `PackagePriority`, `ShipmentStatus`, `RouteType`

```csharp
/// <summary>
/// Factory for creating families of related logistics objects for a specific region
/// </summary>
public interface ILogisticsFactory
{
    Vehicle CreateStandardVehicle(string id, string identifier);
    Vehicle CreateHeavyVehicle(string id, string identifier);
    Vehicle CreateLightVehicle(string id, string identifier);
    Driver CreateDriver(string id, string name, DriverLicenseType licenseType);
    string GetRegion();
    decimal GetRegionalFuelMultiplier();
}
```

---

### **2️⃣ Services Layer (`Lab_2.Services`)**

Contains implementations of **7 structural patterns** (this lab's focus) plus 6 creational patterns (foundation):

#### **STRUCTURAL PATTERNS (Lab 2 - Main Focus):**

**ADAPTER PATTERN - Third-Party Integration:**
- **`GpsTrackingSystem`**, **`RfidTrackingSystem`**, **`BarcodeTrackingSystem`**: Legacy systems with incompatible interfaces
- **`GpsAdapter`**, **`RfidAdapter`**, **`BarcodeAdapter`**: Adapters implementing unified `ITrackingSystem` interface

**DECORATOR PATTERN - Dynamic Enhancements:**
- **`BaseShipment`**: Concrete component implementing `IShipmentComponent`
- **5 Decorators**: `InsuranceDecorator`, `PriorityDecorator`, `TemperatureControlDecorator`, `FragileHandlingDecorator`, `SignatureRequiredDecorator`

**COMPOSITE PATTERN - Hierarchical Structures:**
- **`PackageLeaf`**: Individual package (leaf node)
- **`PackageContainer`**: Container holding packages/containers (composite node)
- **`PackageCompositeBuilder`**: Fluent builder for constructing hierarchies

**FACADE PATTERN - Simplified Interface:**
- **`LogisticsFacade`**: Unified interface coordinating all subsystems (factories, pools, builders, tracking)

**BRIDGE PATTERN - Abstraction/Implementation Separation:**
- **4 Implementors**: `ManualControl`, `AutomaticControl`, `AutonomousControl`, `RemoteControl`
- **3 Abstractions**: `StartVehicleOperation`, `DriveVehicleOperation`, `StopVehicleOperation`

**FLYWEIGHT PATTERN - Memory Optimization:**
- **`PackageType`**: Flyweight storing intrinsic state (category, handling, material, fragility)
- **`PackageTypeFactory`**: Factory managing shared instances
- **`FlyweightPackage`**: Context with extrinsic state

**PROXY PATTERN - Access Control:**
- **`ShipmentReport`**: Real subject (heavy object)
- **3 Proxies**: `LazyShipmentReportProxy` (virtual), `ProtectedShipmentReportProxy` (protection), `CachingShipmentReportProxy` (caching)

---

#### **CREATIONAL PATTERNS (Lab 1 - Foundation):**
- Singleton (`LogisticsConfig`), Abstract Factory (3 regional factories), Factory Method (`VehicleFactory`)
- Builder (`ShipmentBuilder`), Prototype (`RoutePrototypeManager`), Object Pool (`VehiclePool`, `DriverPool`)

---

### **3️⃣ Application Layer (`Lab_2.Application`)**

Demonstrates all **7 structural patterns** with detailed examples:

#### **Demonstration Methods (Focus on Structural Patterns):**
7. **`DemonstrateAdapter()`**: Unifying GPS, RFID, and Barcode tracking systems
8. **`DemonstrateDecorator()`**: Dynamically adding shipment features (insurance, priority, temperature)
9. **`DemonstrateComposite()`**: Building hierarchical package structures
10. **`DemonstrateFacade()`**: Simplified high-level logistics operations
11. **`DemonstrateBridge()`**: Vehicle operations with different control systems
12. **`DemonstrateFlyweight()`**: Memory-efficient package type sharing
13. **`DemonstrateProxy()`**: Report access with lazy loading, protection, and caching
14. **`DemonstrateIntegratedScenario()`**: All patterns working together in real workflow

*Note: Patterns 1-6 (Singleton, Abstract Factory, Factory Method, Builder, Prototype, Object Pool) provide the creational foundation but are not the focus of this lab.*

---

## **Design Decisions & Pattern Selection**

### **Why Logistics/Dispatch Domain?**

The logistics domain was chosen because it naturally demonstrates the need for multiple creational patterns:

- **Complex object hierarchies**: Multi-modal transport (land/sea/air) requires flexible vehicle creation
- **Resource management**: Expensive resources (vehicles, drivers) benefit from pooling
- **Template-based operations**: Routes follow common patterns that can be cloned
- **Regional variations**: Different regions require different logistics strategies
- **Step-by-step construction**: Shipments have many components requiring careful assembly

### **Pattern Selection Rationale**

**Focus: Structural Patterns (Lab 2)**

| Pattern | Why Chosen | Alternative Considered |
|---------|------------|----------------------|
| **Adapter** | Third-party tracking systems have incompatible interfaces | Modify each system's code (violates OCP) |
| **Decorator** | Shipment features should be added/removed dynamically | Subclass explosion (2^5 = 32 combinations!) |
| **Composite** | Packages form natural hierarchies (boxes within boxes) | Separate handling logic (inconsistent) |
| **Facade** | Subsystems are complex and tightly coupled | Expose all complexities (overwhelming) |
| **Bridge** | Vehicle operations should work with any control system | Inheritance hierarchy (4×3 = 12 rigid classes) |
| **Flyweight** | Many packages share common type characteristics | Store all data in each package (wasteful) |
| **Proxy** | Reports are expensive to generate and need access control | Direct access (no lazy loading/protection) |

---

## **Structural Patterns Implementation Details**

This section details the **7 structural patterns** that are the focus of Lab 2:

### **1. Adapter Pattern - Third-Party Tracking System Integration**

**Purpose**: Make incompatible interfaces work together without modifying their source code

**Third-Party Systems (Adaptees)**:
- **`GpsTrackingSystem`**: Returns `GpsCoordinates` objects
- **`RfidTrackingSystem`**: Returns `RfidTag` objects  
- **`BarcodeTrackingSystem`**: Returns `BarcodeData` objects

**Adapters**:
- **`GpsAdapter`**: Converts GPS coordinates to unified `ITrackingSystem` interface
- **`RfidAdapter`**: Converts RFID tags to unified interface
- **`BarcodeAdapter`**: Converts barcode data to unified interface

**Code Example**:
```csharp
ITrackingSystem gpsTracker = new GpsAdapter(new GpsTrackingSystem());
string location = gpsTracker.GetLocation("PKG-001");
// Returns: "Location: 40.7128° N, 74.0060° W"
```

**Key Features**:
- Unified interface for three incompatible systems
- No modification to existing third-party code
- Easy to add new tracking system types

**Benefits**:
- Integrates legacy systems without breaking existing code
- Follows Open/Closed Principle
- Simplifies client code by providing consistent interface

---

### **2. Decorator Pattern - Dynamic Shipment Enhancements**

**Purpose**: Add responsibilities to objects dynamically without modifying their code

**Base Component**:
- **`BaseShipment`**: Concrete shipment with basic cost calculation

**Decorators** (5 types):
- **`InsuranceDecorator`**: Adds 5% insurance cost
- **`PriorityDecorator`**: Adds $50 priority handling fee
- **`TemperatureControlDecorator`**: Adds climate control ($100 + $2/kg)
- **`FragileHandlingDecorator`**: Adds special handling ($30 + $1/kg)
- **`SignatureRequiredDecorator`**: Adds signature confirmation ($15)

**Code Example**:
```csharp
IShipmentComponent shipment = new BaseShipment("SHP-001", "New York", "Los Angeles", 100, 500);

// Stack decorators dynamically
shipment = new InsuranceDecorator(shipment);
shipment = new PriorityDecorator(shipment);
shipment = new TemperatureControlDecorator(shipment, -18);

decimal totalCost = shipment.GetCost();
string description = shipment.GetDescription();
```

**Key Features**:
- Decorators can be stacked in any combination
- Each decorator adds specific functionality
- Original object remains unchanged
- Features can be added/removed at runtime

**Benefits**:
- Flexible alternative to subclassing
- Avoids "class explosion" (2^n combinations)
- Follows Single Responsibility Principle
- Runtime composition instead of compile-time inheritance

---

### **3. Composite Pattern - Hierarchical Package Management**

**Purpose**: Treat individual objects and compositions uniformly in tree structures

**Components**:
- **`IPackageComponent`**: Common interface for leaf and composite
- **`PackageLeaf`**: Individual package (cannot contain children)
- **`PackageContainer`**: Container holding packages/containers (can contain children)
- **`PackageCompositeBuilder`**: Helper for building hierarchies

**Code Example**:
```csharp
// Build hierarchy: Master container → 2 sub-containers → 6 packages
var builder = new PackageCompositeBuilder();
var masterBox = builder
    .CreateContainer("MASTER-001", "Master Shipment Box")
    .AddContainer("BOX-A", "Electronics Box")
        .AddLeaf("PKG-001", "Laptop", 2.5m, PackagePriority.High)
        .AddLeaf("PKG-002", "Phone", 0.5m, PackagePriority.High)
    .EndContainer()
    .AddContainer("BOX-B", "Accessories Box")
        .AddLeaf("PKG-003", "Cables", 0.3m, PackagePriority.Normal)
        .AddLeaf("PKG-004", "Charger", 0.2m, PackagePriority.Normal)
    .EndContainer()
    .Build();

decimal totalWeight = masterBox.GetWeight();        // 3.5 kg
int totalPackages = masterBox.GetPackageCount();    // 4 packages
```

**Key Features**:
- Recursive tree structure (containers within containers)
- Uniform treatment of leaf and composite
- Fluent builder API for easy construction
- Aggregate operations (total weight, count, display)

**Benefits**:
- Represents part-whole hierarchies naturally
- Client code doesn't differentiate between individual/composite
- Easy to add new component types
- Simplifies complex hierarchical structures

---

### **4. Facade Pattern - Simplified Logistics Coordination**

**Purpose**: Provide unified, simplified interface to complex subsystem interactions

**Subsystems Coordinated**:
- Abstract Factory (region selection)
- Vehicle Factory (vehicle creation)
- Vehicle Pool (resource management)
- Driver Pool (driver assignment)
- Shipment Builder (shipment construction)
- Route Prototype Manager (route templates)
- Tracking Systems (GPS/RFID/Barcode adapters)

**Facade Class**:
- **`LogisticsFacade`**: Single entry point for complex logistics operations

**Code Example**:
```csharp
var facade = new LogisticsFacade("Urban");

// One simple call handles: factory selection, vehicle creation, 
// pool management, driver assignment, route cloning, tracking setup
var shipment = facade.CreateCompleteShipment(
    shipmentId: "SHP-FACADE-001",
    packages: packageList,
    routeTemplateId: "urban-template",
    vehicleType: VehicleType.DeliveryTruck,
    trackingType: "GPS"
);
```

**Key Features**:
- Hides subsystem complexity
- Provides high-level operations
- Manages subsystem interactions
- Reduces client dependencies

**Benefits**:
- Simplifies client code dramatically
- Decouples client from subsystems
- Makes subsystem easier to use
- Centralizes complex coordination logic

---

### **5. Bridge Pattern - Vehicle Operations & Control Systems**

**Purpose**: Separate abstraction from implementation so both can vary independently

**Abstraction Hierarchy (Operations)**:
- **`VehicleOperation`** (abstract): Base operation using control system
- **`StartVehicleOperation`**: Refined abstraction for starting
- **`DriveVehicleOperation`**: Refined abstraction for driving
- **`StopVehicleOperation`**: Refined abstraction for stopping

**Implementation Hierarchy (Control Systems)**:
- **`IVehicleControl`**: Implementor interface
- **`ManualControl`**: Human-operated controls
- **`AutomaticControl`**: Automatic transmission/throttle
- **`AutonomousControl`**: Self-driving AI system
- **`RemoteControl`**: Remote operation capability

**Code Example**:
```csharp
// Same operation works with ANY control system
IVehicleControl manualControl = new ManualControl();
IVehicleControl autonomousControl = new AutonomousControl();

VehicleOperation startManual = new StartVehicleOperation(manualControl, "TRUCK-001");
VehicleOperation startAutonomous = new StartVehicleOperation(autonomousControl, "DRONE-001");

startManual.Execute();      // "Starting TRUCK-001 using Manual Control..."
startAutonomous.Execute();  // "Starting DRONE-001 using Autonomous Control..."
```

**Key Features**:
- Operations and control systems vary independently
- Any operation works with any control system
- Easy to add new operations without changing controls
- Easy to add new control systems without changing operations

**Benefits**:
- Avoids rigid inheritance hierarchy (4 operations × 4 controls = 16 classes!)
- Supports runtime switching of implementations
- Follows Open/Closed Principle
- Separates interface from implementation concerns

---

### **6. Flyweight Pattern - Package Type Memory Optimization**

**Purpose**: Share common state among large numbers of objects to reduce memory consumption

**Intrinsic State (Shared - Flyweight)**:
- **`PackageType`**: Category, handling requirements, material, fragility
- Managed by **`PackageTypeFactory`** with 8+ predefined types

**Extrinsic State (Unique - Context)**:
- **`FlyweightPackage`**: Package ID, weight, destination (stored separately)

**Code Example**:
```csharp
var factory = new PackageTypeFactory();

// 1000 packages share only 3 PackageType objects in memory!
var packages = new List<FlyweightPackage>();
for (int i = 0; i < 1000; i++)
{
    PackageType type = factory.GetPackageType("Standard");  // Reuses same instance
    packages.Add(new FlyweightPackage($"PKG-{i}", type, 2.5m, "NYC"));
}

Console.WriteLine($"Total packages: {packages.Count}");
Console.WriteLine($"Shared types: {factory.GetTotalFlyweights()}");  // Only 3!
```

**Predefined Types**:
- Standard, Express, Fragile, Refrigerated, Hazardous, Electronics, Documents, Heavy

**Key Features**:
- Factory ensures type instances are shared
- Intrinsic state stored once, referenced many times
- Extrinsic state stored in context objects
- Massive memory savings for large datasets

**Benefits**:
- Reduces memory footprint dramatically
- Improves cache performance
- Handles large object counts efficiently
- Transparent to client code

---

### **7. Proxy Pattern - Shipment Report Access Control**

**Purpose**: Control access to objects through surrogate placeholder

**Real Subject**:
- **`ShipmentReport`**: Heavy object with expensive PDF generation

**Three Proxy Types**:

**1. Virtual Proxy (Lazy Loading)**:
- **`LazyShipmentReportProxy`**: Defers creation until first access
```csharp
IShipmentReport lazyReport = new LazyShipmentReportProxy("SHP-001");
// No load yet...
lazyReport.Display();  // NOW loads from database (expensive operation)
```

**2. Protection Proxy (Access Control)**:
- **`ProtectedShipmentReportProxy`**: Role-based access control
```csharp
// Employee role - read-only
var employeeReport = new ProtectedShipmentReportProxy("SHP-001", "Employee");
employeeReport.Display();      // ✓ Allowed
employeeReport.ExportToPdf();  // ✗ Access Denied

// Admin role - full access
var adminReport = new ProtectedShipmentReportProxy("SHP-001", "Admin");
adminReport.ExportToPdf();     // ✓ Allowed
```

**3. Caching Proxy (Performance)**:
- **`CachingShipmentReportProxy`**: Caches expensive operations with TTL
```csharp
var cachingReport = new CachingShipmentReportProxy("SHP-001");
cachingReport.ExportToPdf();  // Generates PDF (slow)
cachingReport.ExportToPdf();  // Returns cached PDF (fast!)
// Cache expires after 5 minutes
```

**Key Features**:
- Same interface as real subject (`IShipmentReport`)
- Lazy initialization for expensive objects
- Role-based access control (Employee/Manager/Admin)
- Time-based caching (5-minute TTL)

**Benefits**:
- Controls access to expensive resources
- Adds security without modifying real subject
- Improves performance through caching
- Delays initialization until needed

---

## **Architecture Diagram**

```
┌────────────────────────────────────────────────────────────────────┐
│                        Application Layer                           │
│                      (Lab_2.Application)                           │
│                          Program.cs                                │
│  ┌──────────────────────────────────────────────────────────────┐  │
│  │  LAB 2 FOCUS - STRUCTURAL PATTERNS (7):                      │  │
│  │  • DemonstrateAdapter()       - Unify tracking systems       │  │
│  │  • DemonstrateDecorator()     - Dynamic enhancements         │  │
│  │  • DemonstrateComposite()     - Package hierarchies          │  │
│  │  • DemonstrateFacade()        - Simplified interface         │  │
│  │  • DemonstrateBridge()        - Decouple operations/controls │  │
│  │  • DemonstrateFlyweight()     - Memory optimization          │  │
│  │  • DemonstrateProxy()         - Access control & caching     │  │
│  │                                                              │  │
│  │  • DemonstrateIntegratedScenario() - All patterns together!  │  │
│  │                                                              │  │
│  │  (Foundation: 6 creational patterns from Lab 1)              │  │
│  └──────────────────────────────────────────────────────────────┘  │
└──────────────────────────────┬─────────────────────────────────────┘
                               │ uses
┌──────────────────────────────▼─────────────────────────────────────┐
│                         Services Layer                             │
│                       (Lab_2.Services)                             │
│  ┌─────────────────────────────────────────────────────────────┐   │
│  │ LAB 2 - STRUCTURAL PATTERNS (Main Focus):                   │   │
│  │                                                             │   │
│  │ • Adapter/         - GPS, RFID, Barcode adapters            │   │
│  │ • Decorators/      - 5 shipment decorators                  │   │
│  │ • Composite/       - Package hierarchies                    │   │
│  │ • Facade/          - LogisticsFacade                        │   │
│  │ • Bridge/          - 4 controls × 3 operations              │   │
│  │ • Flyweight/       - PackageType sharing                    │   │
│  │ • Proxy/           - Lazy, Protection, Caching proxies      │   │
│  │                                                             │   │
│  │ LAB 1 - CREATIONAL PATTERNS (Foundation):                   │   │
│  │ • Singleton, Abstract Factory, Factory Method               │   │
│  │ • Builder, Prototype, Object Pool                           │   │
│  └─────────────────────────────────────────────────────────────┘   │
└──────────────────────────────┬─────────────────────────────────────┘
                               │ implements
┌──────────────────────────────▼─────────────────────────────────────┐
│                          Domain Layer                              │
│                        (Lab_2.Domain)                              │
│  ┌───────────────────┐  ┌─────────────────┐  ┌──────────────────┐  │
│  │    Entities       │  │  Interfaces     │  │      Enums       │  │
│  │  • Vehicle (abs)  │  │                 │  │ • VehicleType    │  │
│  │  • DeliveryTruck  │  │ STRUCTURAL:     │  │ • VehicleStatus  │  │
│  │  • CargoTruck     │  │ • ITracking     │  │ • DriverLicense  │  │
│  │  • CargoShip      │  │   System        │  │   Type           │  │
│  │  • ContainerVessel│  │ • IShipment     │  │ • DriverStatus   │  │
│  │  • CargoPlane     │  │   Component     │  │ • PackagePriority│  │
│  │  • Drone          │  │ • IPackage      │  │ • ShipmentStatus │  │
│  │  • Driver         │  │   Component     │  │ • RouteType      │  │
│  │  • Package        │  │ • IVehicle      │  │                  │  │
│  │  • Route          │  │   Control       │  │                  │  │
│  │  • Shipment       │  │ • IShipment     │  │                  │  │
│  │                   │  │   Report        │  │                  │  │
│  │                   │  │                 │  │                  │  │
│  │                   │  │ CREATIONAL:     │  │                  │  │
│  │                   │  │ • ILogistics    │  │                  │  │
│  │                   │  │   Factory       │  │                  │  │
│  │                   │  │ • IVehicle      │  │                  │  │
│  │                   │  │   Factory       │  │                  │  │
│  │                   │  │ • IShipment     │  │                  │  │
│  │                   │  │   Builder       │  │                  │  │
│  │                   │  │ • (+ 3 more)    │  │                  │  │
│  └───────────────────┘  └─────────────────┘  └──────────────────┘  │
└────────────────────────────────────────────────────────────────────┘

  Pattern Integration in Logistics System:
  ┌─────────────────────────────────────────────┐
  │  7 Structural Patterns (FOCUS)              │
  │  - Adapter    - Bridge                      │
  │  - Decorator  - Flyweight                   │
  │  - Composite  - Proxy                       │
  │  - Facade                                   │
  │                                             │
  │  6 Creational Patterns (Foundation)         │
  │  - Singleton  - Builder    - Object Pool    │
  │  - Abstract   - Prototype                   │
  │    Factory                                  │
  │  - Factory                                  │
  │    Method                                   │
  └─────────────────────────────────────────────┘
```

---

## **Conclusions / Results**

In conclusion, **Lab 2** significantly deepened my understanding of **Structural Design Patterns** and their practical applications in real-world software systems. Implementing **7 distinct structural patterns** in a logistics domain demonstrated how these patterns solve complex relationship and composition challenges while maintaining code quality and flexibility.

### **Structural Patterns Deep Dive:**

The **Adapter Pattern** demonstrated how to integrate incompatible third-party systems (GPS, RFID, Barcode) without modifying their code, providing a unified interface that follows the Open/Closed Principle. This pattern proved invaluable for real-world integration scenarios where you cannot change legacy or external systems.

The **Decorator Pattern** showed flexible runtime enhancement of shipments with features like insurance, priority handling, and temperature control. Instead of creating 2^5 = 32 subclasses for all possible combinations, decorators can be stacked dynamically, demonstrating the power of composition over inheritance.

The **Composite Pattern** elegantly handled hierarchical package structures (containers within containers, packages within containers), treating individual packages and containers uniformly through the `IPackageComponent` interface. This pattern naturally represents part-whole hierarchies found in logistics.

The **Facade Pattern** simplified complex subsystem interactions by providing a clean, high-level interface (`LogisticsFacade`) that coordinates multiple services (factories, pools, builders, tracking systems). This dramatically reduced client code complexity and coupling.

The **Bridge Pattern** separated vehicle operations (Start, Drive, Stop) from control systems (Manual, Automatic, Autonomous, Remote), allowing both to vary independently. Without Bridge, we would need 3 operations × 4 controls = 12 tightly coupled classes instead of 3 + 4 = 7 flexible components.

The **Flyweight Pattern** demonstrated massive memory optimization by sharing common `PackageType` data across thousands of packages. Instead of storing category, handling requirements, material, and fragility in each package, these intrinsic properties are shared while only unique extrinsic data (ID, weight, destination) is stored per package.

The **Proxy Pattern** showcased three powerful variants:
- **Virtual Proxy**: Lazy loading of expensive shipment reports
- **Protection Proxy**: Role-based access control (Employee/Manager/Admin)
- **Caching Proxy**: Performance optimization with time-to-live

The integrated scenario demonstrated how **all 7 structural patterns work together** with the 6 creational patterns in a production-grade logistics system. This exemplifies how combining different pattern categories creates robust, scalable, maintainable architectures that follow SOLID principles and professional software engineering best practices.

**Lab 2 Focus**: Structural patterns are about **smart relationships** - how objects work together, how they're composed, how they're accessed, and how complexity is managed through proper organization and abstraction.

### **Example Outputs:**

**Console Output:**
```
╔════════════════════════════════════════════════════════════════╗
║    Creational Design Patterns - Logistics System Demo          ║
║    TMPS Lab 1 - Alexandru Rudoi                                ║
╚════════════════════════════════════════════════════════════════╝

═══════════════════════════════════════════════════════════════
PATTERN 1: SINGLETON - Centralized Configuration
═══════════════════════════════════════════════════════════════
Purpose: Ensure only one instance of configuration exists

config1 == config2: True
Both references point to the SAME instance!

Logistics Configuration:
  Max Load Capacity: 10000.00 kg
  Working Hours: 08:00:00 to 18:00:00
  Max Delivery Distance: 500.00 km
  Vehicle Pool Size: 50
  Driver Pool Size: 100

✓ Singleton pattern ensures single configuration instance
✓ Thread-safe implementation using Lazy<T>

═══════════════════════════════════════════════════════════════
PATTERN 2: ABSTRACT FACTORY - Region-Specific Logistics
═══════════════════════════════════════════════════════════════
Purpose: Create families of related objects (vehicles/drivers) per region

--- Urban Region Factory ---
Standard: DeliveryTruck (DT-Urban-001, 1500.00kg, Urban, Refrigeration: No)
Heavy: CargoPlane (CP-Urban-002, 50000.00kg, Urban, Model: Boeing 777F)
Light: Drone (DRN-Urban-003, 25.00kg, Urban, Autonomous: Yes)
Fuel Multiplier: 1.5x

--- Rural Region Factory ---
Standard: CargoTruck (CT-Rural-001, 8000.00kg, Rural, Axles: 4)
Heavy: CargoTruck (CT-Rural-002, 12000.00kg, Rural, Axles: 5)
Light: CargoTruck (CT-Rural-003, 6000.00kg, Rural, Axles: 3)
Fuel Multiplier: 1.2x

--- International Region Factory ---
Standard: CargoShip (SS-International-001, 100000.00kg, Containers: 200)
Heavy: ContainerVessel (MV-International-002, 250000.00kg, Containers: 1000)
Light: CargoPlane (CP-International-003, 50000.00kg, Model: Airbus A330F)
Fuel Multiplier: 2.0x

✓ Abstract Factory creates region-specific vehicle families
✓ Each factory produces compatible objects for its region

═══════════════════════════════════════════════════════════════
PATTERN 3: FACTORY METHOD - Simple Vehicle Creation
═══════════════════════════════════════════════════════════════
Purpose: Provide interface for creating objects without specifying exact class

Created: DeliveryTruck (DT-ABC-123, 1500.00kg, Urban, Refrigeration: Yes)
Created: CargoTruck (CT-XYZ-789, 8000.00kg, Urban, Axles: 4)
Created: Drone (DRN-456, 25.00kg, Urban, Autonomous: Yes)
Created: CargoShip (SS Logistics, 100000.00kg, Urban, Containers: 200)
Created: CargoPlane (CP-777, 50000.00kg, Urban, Model: Boeing 777F)

✓ Factory Method encapsulates vehicle creation logic
✓ Client code doesn't need to know concrete vehicle classes

═══════════════════════════════════════════════════════════════
PATTERN 4: BUILDER - Complex Shipment Construction
═══════════════════════════════════════════════════════════════
Purpose: Construct complex objects step-by-step with validation

Building shipment step-by-step:
✓ Shipment built: SHP-DEMO-001 (Scheduled, 28.00kg)
  - 3 packages totaling 28.00kg
  - Vehicle: DeliveryTruck (BLD-DT-100)
  - Driver: John Smith
  - Route: Downtown Circuit (30.00km)

✓ Builder pattern enables fluent, step-by-step construction
✓ Validates requirements before creating final object

═══════════════════════════════════════════════════════════════
PATTERN 5: PROTOTYPE - Route Template Cloning
═══════════════════════════════════════════════════════════════
Purpose: Clone existing objects to create new instances efficiently

Available route templates:
  - urban-template
  - rural-template
  - international-template
  - express-template

Cloned urban routes:
Route 1: URBAN-101 - City Center Loop - Morning (Urban, 10.00km, 01:00:00)
Route 2: URBAN-102 - City Center Loop - Evening (Urban, 10.00km, 01:00:00)

Cloned rural route: RURAL-201 - Countryside Circuit - Weekly (Rural, 80.00km, 05:00:00)

✓ Prototype pattern enables efficient object cloning
✓ Templates can be customized after cloning
✓ Avoids expensive initialization for similar objects

═══════════════════════════════════════════════════════════════
PATTERN 6: OBJECT POOL - Resource Management
═══════════════════════════════════════════════════════════════
Purpose: Reuse expensive objects instead of creating/destroying

Vehicle Pool initialized: 4 vehicles
Available: 4

Acquiring vehicles from pool:
  ✓ Acquired: DeliveryTruck (POOL-DT-1, 1500.00kg, Urban)
  ✓ Acquired: Drone (DRN-POOL-DRN-3, 25.00kg, Urban, Autonomous: Yes)

Pool status - Available: 2, In Use: 2

Releasing vehicles back to pool:
  ✓ Released: POOL-DT-1
  ✓ Released: POOL-DRN-3

Pool status - Available: 4, In Use: 0

✓ Object Pool manages resource lifecycle efficiently
✓ Thread-safe acquire/release operations
✓ Reduces object creation overhead

═══════════════════════════════════════════════════════════════
PATTERN 7: INTEGRATED SCENARIO - All Patterns Together
═══════════════════════════════════════════════════════════════
Purpose: Demonstrate how all patterns work together in real scenario

✓ Using Singleton configuration
✓ Created Urban logistics factory
✓ Created 3 vehicles using Factory Method
✓ Initialized Vehicle Pool with 3 vehicles
✓ Cloned route using Prototype: Integrated Demo Route
✓ Created driver pool with 1 drivers
✓ Built complex shipment using Builder pattern

--- Final Shipment Validation ---
✓ Shipment validation PASSED

Shipment Details:
  ID: SHP-INTEGRATED-001
  Status: Scheduled
  Total Weight: 72.00 kg
  Packages: 3
  Vehicle: DeliveryTruck (INT-DT-1, 1500.00kg)
  Driver: Captain Alice Johnson (Aviation license)
  Route: Integrated Demo Route (10.00km, 01:00:00)
  Scheduled: [Current Time + 4 hours]
  Notes: Priority delivery - medical supplies on board

✓ Resources released back to pools

═══════════════════════════════════════════════════════════════
LAB 2: ALL 7 STRUCTURAL PATTERNS SUCCESSFULLY DEMONSTRATED!

STRUCTURAL PATTERNS (Lab 2 Focus):
  1. ✓ Adapter - Third-party tracking unification (GPS, RFID, Barcode)
  2. ✓ Decorator - Dynamic shipment enhancements (5 decorators)
  3. ✓ Composite - Hierarchical package management (trees)
  4. ✓ Facade - Simplified logistics coordination (unified interface)
  5. ✓ Bridge - Operation/control separation (4×3 combinations)
  6. ✓ Flyweight - Shared package type data (memory optimization)
  7. ✓ Proxy - Report access control (lazy, protection, caching)

CREATIONAL PATTERNS (Lab 1 Foundation):
  ✓ Singleton, Abstract Factory, Factory Method
  ✓ Builder, Prototype, Object Pool
═══════════════════════════════════════════════════════════════

╔════════════════════════════════════════════════════════════════╗
║  Lab 2: 7 Structural Design Patterns Successfully Mastered!   ║
║  Building Flexible Relationships & Compositions in Logistics  ║
╚════════════════════════════════════════════════════════════════╝

Press any key to exit...
```

---

## **How to Run**

### **Prerequisites:**
- .NET 9.0 SDK installed
- Visual Studio 2022, Rider, or VS Code with C# extension

### **Build and Run:**

**Using Command Line:**
```cmd
cd Lab_2
dotnet restore
dotnet build
dotnet run --project Lab_2.Application
```

**Using Visual Studio/Rider:**
1. Open `Lab_2.sln`
2. Set `Lab_2.Application` as startup project
3. Press F5 or click Run

### **Project Structure:**
```
Lab_2/
├── Lab_2.sln                          # Solution file
├── README.md                          # This file - Structural Patterns documentation
├── Lab_2.Domain/                      # Domain layer
│   ├── Entities/                      # Business entities (Vehicle, Driver, Package, etc.)
│   ├── Enums/                         # Enumeration types
│   ├── Interfaces/                    # Pattern contracts
│   │   ├── ITrackingSystem.cs        # Adapter pattern target
│   │   ├── IShipmentComponent.cs     # Decorator pattern component
│   │   ├── IPackageComponent.cs      # Composite pattern component
│   │   ├── IVehicleControl.cs        # Bridge pattern implementor
│   │   ├── IShipmentReport.cs        # Proxy pattern subject
│   │   └── (+ 6 creational interfaces)
├── Lab_2.Services/                    # Services layer
│   ├── Adapters/                      # LAB 2: Adapter (GPS, RFID, Barcode)
│   ├── Decorators/                    # LAB 2: Decorator (5 types)
│   ├── Composite/                     # LAB 2: Composite (Leaf, Container, Builder)
│   ├── Facade/                        # LAB 2: Facade (LogisticsFacade)
│   ├── Bridge/                        # LAB 2: Bridge (4 controls + 3 operations)
│   ├── Flyweight/                     # LAB 2: Flyweight (PackageType sharing)
│   ├── Proxy/                         # LAB 2: Proxy (Lazy, Protection, Caching)
│   ├── Factories/                     # LAB 1: Abstract Factory, Factory Method
│   ├── Builders/                      # LAB 1: Builder pattern
│   ├── Prototypes/                    # LAB 1: Prototype pattern
│   ├── Pools/                         # LAB 1: Object Pool pattern
│   └── Configuration/                 # LAB 1: Singleton pattern
└── Lab_2.Application/                 # Application layer
    └── Program.cs                     # 7 structural + 6 creational demonstrations
```

---

## **References**

- **Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides** – Design Patterns: Elements of Reusable Object-Oriented Software (Gang of Four)
- **Robert C. Martin** – Clean Architecture: A Craftsman's Guide to Software Structure and Design
- **Refactoring.Guru** – Creational Design Patterns ([refactoring.guru/design-patterns/creational-patterns](https://refactoring.guru/design-patterns/creational-patterns))
- **Microsoft Docs** – C# .NET 9 Documentation ([docs.microsoft.com/en-us/dotnet/](https://docs.microsoft.com/en-us/dotnet/))
- **Microsoft Docs** – Design Patterns in .NET ([docs.microsoft.com/en-us/azure/architecture/patterns/](https://docs.microsoft.com/en-us/azure/architecture/patterns/))
- **Source Making** – Design Patterns ([sourcemaking.com/design_patterns](https://sourcemaking.com/design_patterns))