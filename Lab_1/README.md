# **Creational Design Patterns Implementation in Logistics System**

### **Course**: Software Design Techniques and Mechanisms

### **Author**: Alexandru Rudoi

---

## **Theory**

**Creational Design Patterns** are design patterns that deal with object creation mechanisms, trying to create objects in a manner suitable to the situation. The basic form of object creation could result in design problems or added complexity to the design. Creational design patterns solve this problem by controlling the object creation process. This project implements **six essential creational patterns**:

### **1. Singleton Pattern**
Ensures a class has **only one instance** and provides a global point of access to it. Used for managing shared resources like configuration settings or connection pools.

### **2. Abstract Factory Pattern**  
Provides an interface for creating **families of related or dependent objects** without specifying their concrete classes. Allows systems to be independent of how their objects are created, composed, and represented.

### **3. Factory Method Pattern**
Defines an interface for creating an object, but lets **subclasses decide which class to instantiate**. Factory Method lets a class defer instantiation to subclasses.

### **4. Builder Pattern**
Separates the construction of a complex object from its representation, allowing the **same construction process to create different representations**. Useful for creating objects with many optional parameters.

### **5. Prototype Pattern**
Specifies the kinds of objects to create using a prototypical instance, and creates new objects by **copying this prototype**. Useful when object creation is expensive or complex.

### **6. Object Pool Pattern**
Uses a set of initialized objects kept ready to use, rather than allocating and destroying them on demand. Improves **performance and resource management** when objects are expensive to create.

The implementation uses **Clean Architecture** with clear separation of concerns across multiple layers, demonstrating how creational patterns create flexible, maintainable, and efficient software systems.

---

## **Objectives**

- Implement **Clean Architecture** with:
  - **Domain Layer**: Core entities, interfaces, and enums
  - **Services Layer**: Pattern implementations and business logic  
  - **Application Layer**: Pattern demonstrations and orchestration

- Demonstrate **Singleton Pattern** by:
  - Creating thread-safe single instance for logistics configuration
  - Providing centralized access to shared settings

- Demonstrate **Abstract Factory Pattern** by:
  - Creating region-specific factories (Urban, Rural, International)
  - Producing families of related objects (vehicles and drivers per region)

- Demonstrate **Factory Method Pattern** by:
  - Encapsulating vehicle creation logic
  - Creating six types of logistics vehicles without exposing instantiation

- Demonstrate **Builder Pattern** by:
  - Constructing complex shipment objects step-by-step
  - Implementing fluent interface with validation

- Demonstrate **Prototype Pattern** by:
  - Cloning route templates efficiently
  - Customizing cloned objects for specific scenarios

- Demonstrate **Object Pool Pattern** by:
  - Managing reusable vehicle and driver resources
  - Implementing thread-safe acquire/release mechanisms

---

## **Implementation Description**

My logistics/dispatch system consists of three architectural layers implementing six creational patterns:

### **1️⃣ Domain Layer (`Lab_1.Domain`)**

Contains the core business entities, contracts, and type definitions:

#### **Entities:**
- **`Vehicle`** (abstract): Base class for all transport vehicles with cloning capability
- **`DeliveryTruck`**, **`CargoTruck`**, **`CargoShip`**, **`ContainerVessel`**, **`CargoPlane`**, **`Drone`**: Concrete multi-modal transport vehicles
- **`Driver`**: Vehicle operator with license validation for different vehicle types
- **`Package`**: Cargo item with weight, priority, and destination information
- **`Route`**: Delivery route with waypoints and cloning support
- **`Shipment`**: Complex aggregate of packages, vehicle, driver, and route

#### **Interfaces:**
- **`ILogisticsFactory`**: Abstract Factory contract for region-specific object creation
- **`IVehicleFactory`**: Factory Method contract for vehicle creation
- **`IShipmentBuilder`**: Builder contract for complex shipment construction
- **`IRoutePrototype`**: Prototype contract for route cloning
- **`IResourcePool<T>`**: Pool contract for resource management
- **`ILogisticsConfig`**: Singleton contract for configuration access

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

### **2️⃣ Services Layer (`Lab_1.Services`)**

Contains concrete implementations of all six creational patterns:

#### **SINGLETON PATTERN - Configuration Management:**
- **`LogisticsConfig`**: Thread-safe singleton using `Lazy<T>` for centralized settings

```csharp
/// <summary>
/// Singleton pattern - ensures only one configuration instance exists
/// Thread-safe implementation using Lazy<T>
/// </summary>
public class LogisticsConfig : ILogisticsConfig
{
    private static readonly Lazy<LogisticsConfig> _instance = 
        new Lazy<LogisticsConfig>(() => new LogisticsConfig());

    private LogisticsConfig() { /* Private constructor */ }

    public static LogisticsConfig Instance => _instance.Value;
    
    // Configuration properties...
}
```

#### **ABSTRACT FACTORY PATTERN - Regional Logistics:**
- **`UrbanLogisticsFactory`**: Creates delivery trucks, drones, and cargo planes for city logistics
- **`RuralLogisticsFactory`**: Creates heavy cargo trucks for countryside transport
- **`InternationalLogisticsFactory`**: Creates ships, vessels, and planes for global logistics

#### **FACTORY METHOD PATTERN - Vehicle Creation:**
- **`VehicleFactory`**: Encapsulates creation of six vehicle types with region-specific parameters

```csharp
public Vehicle CreateDeliveryTruck(string id, string licensePlate, 
    decimal capacity, bool hasRefrigeration)
{
    return new DeliveryTruck(id, licensePlate, capacity, _region, hasRefrigeration);
}
```

#### **BUILDER PATTERN - Complex Construction:**
- **`ShipmentBuilder`**: Fluent interface for step-by-step shipment construction with validation

```csharp
var shipment = builder
    .SetId("SHP-001")
    .AddPackages(packages)
    .AssignVehicle(vehicle)
    .AssignDriver(driver)
    .SetRoute(route)
    .SetScheduledDate(DateTime.Now.AddDays(1))
    .Build(); // Validates before returning
```

#### **PROTOTYPE PATTERN - Template Cloning:**
- **`RoutePrototypeManager`**: Manages route templates and provides cloning with modifications

#### **OBJECT POOL PATTERN - Resource Management:**
- **`VehiclePool`**: Thread-safe pool for reusing vehicle objects
- **`DriverPool`**: Thread-safe pool for reusing driver objects

---

### **3️⃣ Application Layer (`Lab_1.Application`)**

Orchestrates comprehensive demonstrations of all six patterns:

#### **Seven demonstration methods:**
1. **`DemonstrateSingleton()`**: Shows single instance guarantee
2. **`DemonstrateAbstractFactory()`**: Creates region-specific object families
3. **`DemonstrateFactoryMethod()`**: Simple vehicle creation
4. **`DemonstrateBuilder()`**: Step-by-step shipment construction
5. **`DemonstratePrototype()`**: Route template cloning
6. **`DemonstrateObjectPool()`**: Resource acquire/release
7. **`DemonstrateIntegratedScenario()`**: All patterns working together

```csharp
// Integrated scenario showing all patterns in harmony
var config = LogisticsConfig.Instance;                    // Singleton
ILogisticsFactory factory = new UrbanLogisticsFactory();  // Abstract Factory
var vehicleFactory = new VehicleFactory("International"); // Factory Method
var vehiclePool = new VehiclePool(vehicles);              // Object Pool
var route = routeManager.CloneTemplate("urban-template"); // Prototype
var shipment = builder.SetId("SHP-001")...Build();       // Builder
```

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

| Pattern | Why Chosen | Alternative Considered |
|---------|------------|----------------------|
| **Singleton** | Configuration should be globally accessible and consistent | Static class (lacks interface/inheritance) |
| **Abstract Factory** | Region-specific logistics need coordinated object families | Multiple Factory Methods (less cohesive) |
| **Factory Method** | Simple vehicle creation needs encapsulation | Direct instantiation (exposes complexity) |
| **Builder** | Shipments have many optional/required fields needing validation | Telescoping constructors (unreadable) |
| **Prototype** | Routes share structure but need customization | Always creating from scratch (inefficient) |
| **Object Pool** | Vehicles/drivers are expensive to create and can be reused | Create/destroy on demand (wasteful) |

### **SOLID Principles in Creational Patterns**

Even though this lab focuses on creational patterns, SOLID principles are still applied:

- **SRP**: Each factory/builder has single creation responsibility
- **OCP**: New vehicle types can be added without modifying factories
- **LSP**: All vehicles are substitutable for base `Vehicle` class
- **ISP**: Separate interfaces for each pattern (not one giant interface)
- **DIP**: Application depends on abstractions (`ILogisticsFactory`, `IVehicleFactory`, etc.), not concrete implementations

---

## **Creational Patterns Implementation Details**

### **1. Singleton Pattern - LogisticsConfig**

**Purpose**: Ensure only one configuration instance exists globally

**Key Features**:
- Thread-safe implementation using `Lazy<T>`
- Private constructor prevents external instantiation
- Static `Instance` property provides global access point
- Manages shared settings like max load capacity, working hours, and pool sizes

**Benefits**:
- Centralized configuration management
- Memory efficient - single instance
- Thread-safe without performance overhead

**Code Example**:
```csharp
var config1 = LogisticsConfig.Instance;
var config2 = LogisticsConfig.Instance;
// config1 == config2 → TRUE (same instance!)
```

---

### **2. Abstract Factory Pattern - Regional Logistics Factories**

**Purpose**: Create families of related objects without specifying concrete classes

**Implementations**:
- **`UrbanLogisticsFactory`**: Produces delivery trucks, drones, cargo planes (city/fast delivery)
- **`RuralLogisticsFactory`**: Produces heavy cargo trucks (countryside/bulk transport)
- **`InternationalLogisticsFactory`**: Produces ships, vessels, planes (global/long-distance)

**Key Features**:
- Each factory produces compatible object families
- Region-specific fuel multipliers and vehicle characteristics
- Consistent interface across all factories

**Benefits**:
- Easy to add new regional factories
- Objects from same factory work together seamlessly
- Client code doesn't depend on concrete vehicle classes

---

### **3. Factory Method Pattern - VehicleFactory**

**Purpose**: Encapsulate object creation without specifying exact classes

**Supported Vehicles**:
- `CreateDeliveryTruck()` - Small urban deliveries
- `CreateCargoTruck()` - Heavy land transport
- `CreateCargoShip()` - Sea freight
- `CreateContainerVessel()` - Large-scale maritime
- `CreateCargoPlane()` - Air cargo
- `CreateDrone()` - Autonomous light delivery

**Key Features**:
- Single point of vehicle creation
- Region parameter for customization
- Hides complex instantiation logic

**Benefits**:
- Client code doesn't need to know concrete classes
- Easy to add new vehicle types
- Centralized creation logic simplifies maintenance

---

### **4. Builder Pattern - ShipmentBuilder**

**Purpose**: Construct complex objects step-by-step with validation

**Fluent Interface**:
```csharp
var shipment = builder
    .SetId("SHP-001")              // Required
    .AddPackage(package1)          // At least one required
    .AddPackages(packageList)      // Bulk add
    .AssignVehicle(vehicle)        // Required
    .AssignDriver(driver)          // Required
    .SetRoute(route)               // Required
    .SetScheduledDate(date)        // Optional
    .AddNotes("Handle with care")  // Optional
    .Build();                      // Validates & constructs
```

**Key Features**:
- Method chaining for readable construction
- Validation in `Build()` ensures complete shipments
- Calculates total weight and checks vehicle capacity
- Validates driver can operate assigned vehicle

**Benefits**:
- Separates construction from representation
- Makes complex object creation manageable
- Enforces business rules before object creation

---

### **5. Prototype Pattern - RoutePrototypeManager**

**Purpose**: Clone existing objects instead of creating from scratch

**Pre-built Templates**:
- **Urban Template**: City routes (10km, 8 stops)
- **Rural Template**: Countryside routes (80km, 4 stops)
- **International Template**: Long-haul routes (500km, 3 stops)
- **Express Template**: Fast delivery routes (15km, 3 stops)

**Key Features**:
- `CloneTemplate()` - Direct clone
- `CloneWithModifications()` - Clone with custom ID/name
- Deep copying of waypoints and properties

**Benefits**:
- Efficient creation of similar objects
- Avoids expensive initialization
- Templates can be customized after cloning

---

### **6. Object Pool Pattern - Vehicle & Driver Pools**

**Purpose**: Reuse expensive objects instead of constant creation/destruction

**Implementation**:
- **`VehiclePool`**: Manages vehicle availability
- **`DriverPool`**: Manages driver availability

**Key Operations**:
```csharp
// Acquire from pool
var vehicle = pool.Acquire();
var drone = pool.AcquireByType(VehicleType.Drone);

// Use the resource...

// Release back to pool
pool.Release(vehicle);
```

**Key Features**:
- Thread-safe acquire/release operations
- Status tracking (Available/InUse)
- Type-specific acquisition
- License-based driver acquisition

**Benefits**:
- Reduces object creation overhead
- Controls resource usage
- Improves performance for expensive objects
- Automatic status management

---

## **Architecture Diagram**

```
┌────────────────────────────────────────────────────────────────────┐
│                        Application Layer                           │
│                      (Lab_1.Application)                           │
│                          Program.cs                                │
│  ┌──────────────────────────────────────────────────────────────┐  │
│  │  • DemonstrateSingleton()        • DemonstratePrototype()    │  │
│  │  • DemonstrateAbstractFactory()  • DemonstrateObjectPool()   │  │
│  │  • DemonstrateFactoryMethod()    • IntegratedScenario()      │  │
│  │  • DemonstrateBuilder()                                      │  │
│  └──────────────────────────────────────────────────────────────┘  │
└──────────────────────────────┬─────────────────────────────────────┘
                               │ uses
┌──────────────────────────────▼─────────────────────────────────────┐
│                         Services Layer                             │
│                       (Lab_1.Services)                             │
│  ┌─────────────────────────────────────────────────────────────┐   │
│  │ CREATIONAL PATTERNS IMPLEMENTATION:                         │   │
│  │                                                             │   │
│  │ [Singleton]          [Abstract Factory]                     │   │
│  │ • LogisticsConfig    • UrbanLogisticsFactory                │   │
│  │                      • RuralLogisticsFactory                │   │
│  │                      • InternationalLogisticsFactory        │   │
│  │                                                             │   │
│  │ [Factory Method]     [Builder]                              │   │
│  │ • VehicleFactory     • ShipmentBuilder                      │   │
│  │                                                             │   │
│  │ [Prototype]          [Object Pool]                          │   │
│  │ • RoutePrototype     • VehiclePool                          │   │
│  │   Manager            • DriverPool                           │   │
│  └─────────────────────────────────────────────────────────────┘   │
└──────────────────────────────┬─────────────────────────────────────┘
                               │ implements
┌──────────────────────────────▼─────────────────────────────────────┐
│                          Domain Layer                              │
│                        (Lab_1.Domain)                              │
│  ┌───────────────────┐  ┌─────────────────┐  ┌──────────────────┐  │
│  │    Entities       │  │   Interfaces    │  │      Enums       │  │
│  │  • Vehicle (abs)  │  │ • ILogistics    │  │ • VehicleType    │  │
│  │  • DeliveryTruck  │  │   Factory       │  │ • VehicleStatus  │  │
│  │  • CargoTruck     │  │ • IVehicle      │  │ • DriverLicense  │  │
│  │  • CargoShip      │  │   Factory       │  │   Type           │  │
│  │  • ContainerVessel│  │ • IShipment     │  │ • DriverStatus   │  │
│  │  • CargoPlane     │  │   Builder       │  │ • PackagePriority│  │
│  │  • Drone          │  │ • IRoutePro     │  │ • ShipmentStatus │  │
│  │  • Driver         │  │   totype        │  │ • RouteType      │  │
│  │  • Package        │  │ • IResourcePool │  │                  │  │
│  │  • Route          │  │ • ILogistics    │  │                  │  │
│  │  • Shipment       │  │   Config        │  │                  │  │
│  └───────────────────┘  └─────────────────┘  └──────────────────┘  │
└────────────────────────────────────────────────────────────────────┘

  Multi-Modal Transport System:
  ┌──────────┐  ┌──────────┐  ┌──────────┐
  │   LAND   │  │   SEA    │  │   AIR    │
  ├──────────┤  ├──────────┤  ├──────────┤
  │ Delivery │  │  Cargo   │  │  Cargo   │
  │  Truck   │  │  Ship    │  │  Plane   │
  │  Cargo   │  │Container │  │  Drone   │
  │  Truck   │  │  Vessel  │  │          │
  └──────────┘  └──────────┘  └──────────┘
```

---

## **Conclusions / Results**

In conclusion, this lab significantly deepened my understanding of **Creational Design Patterns** and their practical applications in real-world software systems. Implementing six distinct patterns in a logistics domain demonstrated how each pattern solves specific object creation challenges while maintaining code quality and flexibility.

The **Singleton Pattern** showed how to manage shared resources efficiently with thread-safe single instance access. The **Abstract Factory** and **Factory Method** patterns demonstrated different levels of abstraction in object creation - Abstract Factory for creating families of related objects (regional logistics) and Factory Method for individual object creation with encapsulation.

The **Builder Pattern** proved invaluable for constructing complex objects (shipments) with many optional parameters while enforcing validation rules. The **Prototype Pattern** demonstrated efficient object cloning for route templates, avoiding expensive initialization. Finally, the **Object Pool Pattern** showcased professional resource management for expensive objects like vehicles and drivers.

This hands-on implementation reinforced that creational patterns are not just theoretical concepts but practical solutions to common design problems. The Clean Architecture approach with separated layers (Domain, Services, Application) made the patterns more visible and maintainable. Each pattern has its specific use case:
- Use **Singleton** for shared configuration/resources
- Use **Abstract Factory** for platform/region-specific object families  
- Use **Factory Method** for simple object creation without coupling
- Use **Builder** for complex objects with many parameters
- Use **Prototype** for expensive object cloning
- Use **Object Pool** for reusable expensive resources

The integrated scenario demonstrated how all six patterns can work together harmoniously in a production system, each solving its specific challenge while contributing to an elegant, maintainable architecture.

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
ALL 6 CREATIONAL PATTERNS SUCCESSFULLY INTEGRATED:
  1. ✓ Singleton - Configuration management
  2. ✓ Abstract Factory - Region-specific logistics
  3. ✓ Factory Method - Vehicle creation
  4. ✓ Builder - Complex shipment construction
  5. ✓ Prototype - Route template cloning
  6. ✓ Object Pool - Resource lifecycle management
═══════════════════════════════════════════════════════════════

╔════════════════════════════════════════════════════════════════╗
║  All Creational Design Patterns Successfully Demonstrated!     ║
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
cd Lab_1
dotnet restore
dotnet build
dotnet run --project Lab_1.Application
```

**Using Visual Studio/Rider:**
1. Open `Lab_1.sln`
2. Set `Lab_1.Application` as startup project
3. Press F5 or click Run

### **Project Structure:**
```
Lab_1/
├── Lab_1.sln                          # Solution file
├── Lab_1.Domain/                      # Domain layer
│   ├── Entities/                      # Business entities
│   │   ├── Vehicles/                  # Vehicle hierarchy
│   │   ├── Driver.cs, Package.cs, etc.
│   ├── Enums/                         # Enumeration types
│   ├── Interfaces/                    # Contracts
│   └── Factory/                       # Factory interfaces
├── Lab_1.Services/                    # Services layer
│   ├── Factories/                     # Factory implementations
│   ├── Builders/                      # Builder implementations
│   ├── Prototypes/                    # Prototype implementations
│   ├── Pools/                         # Pool implementations
│   ├── Configuration/                 # Singleton implementation
│   └── ShipmentService.cs
└── Lab_1.Application/                 # Application layer
    └── Program.cs                     # Pattern demonstrations
```

---

## **References**

- **Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides** – Design Patterns: Elements of Reusable Object-Oriented Software (Gang of Four)
- **Robert C. Martin** – Clean Architecture: A Craftsman's Guide to Software Structure and Design
- **Refactoring.Guru** – Creational Design Patterns ([refactoring.guru/design-patterns/creational-patterns](https://refactoring.guru/design-patterns/creational-patterns))
- **Microsoft Docs** – C# .NET 9 Documentation ([docs.microsoft.com/en-us/dotnet/](https://docs.microsoft.com/en-us/dotnet/))
- **Microsoft Docs** – Design Patterns in .NET ([docs.microsoft.com/en-us/azure/architecture/patterns/](https://docs.microsoft.com/en-us/azure/architecture/patterns/))
- **Source Making** – Design Patterns ([sourcemaking.com/design_patterns](https://sourcemaking.com/design_patterns))