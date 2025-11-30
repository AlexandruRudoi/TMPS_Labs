# **Behavioral Design Patterns Implementation in Logistics System**

### **Course**: Software Design Techniques and Mechanisms

### **Author**: Alexandru Rudoi

---

## **Theory**

This project demonstrates **3 Behavioral Design Patterns** in a logistics/dispatch system using Clean Architecture. Behavioral patterns are concerned with algorithms and the assignment of responsibilities between objects. They describe not just patterns of objects or classes but also the patterns of communication between them.

### **Behavioral Design Patterns Overview**

**Behavioral Design Patterns** focus on communication between objects, what goes on between objects and how they operate together. They characterize the ways in which classes or objects interact and distribute responsibility:

### **Available Behavioral Patterns:**

1. **Chain of Responsibility** - Pass requests along a chain of handlers
2. **Command** - Encapsulate requests as objects
3. **Interpreter** - Define a grammatical representation for a language
4. **Iterator** - Access elements of a collection sequentially
5. **Mediator** - Define simplified communication between classes
6. **Memento** - Capture and restore an object's internal state
7. **Observer** - Define a subscription mechanism to notify multiple objects
8. **State** - Alter an object's behavior when its internal state changes
9. **Strategy** - Define a family of algorithms and make them interchangeable
10. **Template Method** - Define skeleton of algorithm, defer steps to subclasses
11. **Visitor** - Separate algorithms from objects on which they operate

### **Implemented Patterns (Lab 3 Focus):**

### **1. Observer Pattern**
Defines a **one-to-many dependency** between objects so that when one object changes state, all its dependents are notified and updated automatically. Perfect for event handling and notification systems.

### **2. Strategy Pattern**
Defines a **family of algorithms**, encapsulates each one, and makes them interchangeable. Strategy lets the algorithm vary independently from clients that use it.

### **3. State Pattern**
Allows an object to **alter its behavior when its internal state changes**. The object will appear to change its class. State-specific behavior is encapsulated in separate state classes.

---

The implementation uses **Clean Architecture** with clear separation of concerns across multiple layers, demonstrating how behavioral patterns enable flexible communication, runtime algorithm selection, and elegant state management in complex software systems.

---

## **Objectives**

- Implement **Clean Architecture** with:
  - **Domain Layer**: Core entities, interfaces, and enums
  - **Services Layer**: Pattern implementations and business logic  
  - **Application Layer**: Pattern demonstrations and orchestration

- Demonstrate **3 Behavioral Patterns** (Lab 3 Focus):
  - **Observer**: Shipment status notification system with multiple observer types (Email, SMS, Dashboard, Logging)
  - **Strategy**: Dynamic delivery pricing with interchangeable strategies (Standard, Express, Economy, Overnight)
  - **State**: Shipment lifecycle state machine with proper state transitions (Pending → In-Transit → Out-for-Delivery → Delivered/Cancelled)

- Foundation: **7 Structural Patterns** (from Lab 2) and **6 Creational Patterns** (from Lab 1) provide the underlying architecture

---

## **Implementation Description**

My logistics/dispatch system implements **3 behavioral design patterns** (Lab 3) built on top of a foundation of structural and creational patterns from previous labs. The system uses three architectural layers:

### **1️⃣ Domain Layer (`Lab_3.Domain`)**

Contains the core business entities, contracts, and type definitions:

#### **Entities:**
- **`Vehicle`** (abstract): Base class for all transport vehicles
- **`DeliveryTruck`**, **`CargoTruck`**, **`CargoShip`**, **`ContainerVessel`**, **`CargoPlane`**, **`Drone`**: Concrete vehicles
- **`Driver`**, **`Package`**, **`Route`**, **`Shipment`**: Supporting entities

#### **Interfaces:**
- **Behavioral Pattern Interfaces** (Lab 3 focus):
  - `IShipmentObserver` - Observer pattern interface for status notifications
  - `IPricingStrategy` - Strategy pattern interface for pricing algorithms
  - `IShipmentState` - State pattern interface for lifecycle states
- **Structural Pattern Interfaces** (Lab 2) - `ITrackingSystem`, `IShipmentComponent`, `IPackageComponent`, `IVehicleControl`, `IShipmentReport`
- **Creational Pattern Interfaces** (Lab 1) - `ILogisticsFactory`, `IVehicleFactory`, `IShipmentBuilder`, `IRoutePrototype`, `IResourcePool<T>`

#### **Enums:**
Seven enums in separate files: `VehicleType`, `VehicleStatus`, `DriverLicenseType`, `DriverStatus`, `PackagePriority`, `ShipmentStatus`, `RouteType`

---

### **2️⃣ Services Layer (`Lab_3.Services`)**

Contains implementations of **3 behavioral patterns** (this lab's focus) plus structural and creational patterns from previous labs:

#### **BEHAVIORAL PATTERNS (Lab 3 - Main Focus):**

**OBSERVER PATTERN - Shipment Status Notifications:**
- **`IShipmentObserver`** (Domain.Interfaces): Observer interface for status change notifications
- **`ShipmentTracker`**: Subject that maintains list of observers and notifies them
- **Concrete Observers**:
  - **`EmailNotificationObserver`**: Sends email notifications
  - **`SmsNotificationObserver`**: Sends SMS messages
  - **`DashboardObserver`**: Updates monitoring dashboard
  - **`LoggingObserver`**: Maintains audit trail logs

**STRATEGY PATTERN - Dynamic Pricing:**
- **`IPricingStrategy`** (Domain.Interfaces): Strategy interface for price calculation
- **`DeliveryPricingContext`**: Context class that uses a strategy
- **Concrete Strategies**:
  - **`StandardPricingStrategy`**: Balanced pricing for regular deliveries
  - **`ExpressPricingStrategy`**: Premium rates for fast delivery
  - **`EconomyPricingStrategy`**: Budget-friendly flexible delivery
  - **`OvernightPricingStrategy`**: Premium overnight delivery rates

**STATE PATTERN - Shipment Lifecycle:**
- **`IShipmentState`** (Domain.Interfaces): State interface defining state-specific behavior
- **`ShipmentContext`**: Context maintaining current state
- **Concrete States**:
  - **`PendingState`**: Initial state, shipment being prepared
  - **`InTransitState`**: Shipment in transport
  - **`OutForDeliveryState`**: With delivery driver
  - **`DeliveredState`**: Final successful state (terminal)
  - **`CancelledState`**: Cancelled shipment (terminal)

---

#### **STRUCTURAL PATTERNS (Lab 2 - Foundation):**
- Adapter, Decorator, Composite, Facade, Bridge, Flyweight, Proxy

#### **CREATIONAL PATTERNS (Lab 1 - Foundation):**
- Singleton, Abstract Factory, Factory Method, Builder, Prototype, Object Pool

---

### **3️⃣ Application Layer (`Lab_3.Application`)**

Demonstrates all **3 behavioral patterns** with detailed examples:

#### **Demonstration Methods (Focus on Behavioral Patterns):**
1. **`DemonstrateObserver()`**: Shipment tracking notification system with multiple observers
2. **`DemonstrateStrategy()`**: Dynamic pricing calculation with different strategies
3. **`DemonstrateState()`**: Shipment lifecycle state machine with transitions

*Note: Structural patterns (Lab 2) and Creational patterns (Lab 1) remain in the codebase as foundation but are not demonstrated in the main application flow for Lab 3.*

---

## **Design Decisions & Pattern Selection**

### **Why Logistics/Dispatch Domain?**

The logistics domain was chosen because it naturally demonstrates the need for behavioral patterns:

- **Event-driven notifications**: Shipments change status frequently, requiring notification to multiple parties
- **Variable pricing algorithms**: Different delivery speeds and services require different pricing strategies
- **State-dependent behavior**: Shipments have distinct lifecycle states with different allowed operations
- **Complex workflows**: Real-world logistics involves coordination between multiple systems and stakeholders

### **Pattern Selection Rationale**

**Focus: Behavioral Patterns (Lab 3)**

| Pattern | Why Chosen | Alternative Considered |
|---------|------------|----------------------|
| **Observer** | Shipment status changes need to notify multiple parties (customers, dashboards, logs) | Polling (inefficient, high latency) |
| **Strategy** | Pricing varies based on delivery speed/service level | if-else chains (rigid, hard to extend) |
| **State** | Shipments have clear lifecycle states with different behaviors | Status flags + conditionals (complex, error-prone) |

---

## **Behavioral Patterns Implementation Details**

This section details the **3 behavioral patterns** that are the focus of Lab 3:

### **1. Observer Pattern - Shipment Status Notification System**

**Purpose**: Define a one-to-many dependency where multiple observers are notified when the subject's state changes

**Subject**:
- **`ShipmentTracker`**: Maintains list of observers, manages shipment status, notifies observers on changes

**Observer Interface**:
- **`IShipmentObserver`**: Common interface with `OnShipmentStatusChanged()` method

**Concrete Observers** (4 types):
- **`EmailNotificationObserver`**: Sends email notifications to specified address
- **`SmsNotificationObserver`**: Sends SMS messages to phone number
- **`DashboardObserver`**: Updates monitoring dashboard displays
- **`LoggingObserver`**: Maintains audit trail with timestamps

**Code Example**:
```csharp
// Create subject
var tracker = new ShipmentTracker();

// Create and attach observers
tracker.Attach(new EmailNotificationObserver("customer@example.com"));
tracker.Attach(new SmsNotificationObserver("+1-555-0123"));
tracker.Attach(new DashboardObserver("MAIN-001"));
tracker.Attach(new LoggingObserver());

// Update status - all observers automatically notified!
tracker.UpdateShipmentStatus("SHP-001", "In-Transit", "Distribution Center - Philadelphia");

// Output:
// [EMAIL] To: customer@example.com - Shipment SHP-001 is now 'In-Transit'
// [SMS] To: +1-555-0123 - Shipment SHP-001 is now 'In-Transit' at Distribution Center
// [DASHBOARD-MAIN-001] Shipment SHP-001 updated - Status: In-Transit
// [LOG] [2025-11-30 14:30:22] Shipment SHP-001: Pending -> In-Transit @ Distribution Center
```

**Key Features**:
- Dynamic observer attachment/detachment at runtime
- Loose coupling between subject and observers
- One status change triggers multiple notifications
- Each observer handles notification in its own way

**Benefits**:
- Loose coupling - subject doesn't know concrete observer types
- Open/Closed Principle - easy to add new observer types
- Broadcast communication - one event notifies many
- Runtime subscription management

---

### **2. Strategy Pattern - Dynamic Delivery Pricing**

**Purpose**: Define a family of interchangeable algorithms and make them selectable at runtime

**Context**:
- **`DeliveryPricingContext`**: Uses a pricing strategy and allows strategy switching

**Strategy Interface**:
- **`IPricingStrategy`**: Common interface with `CalculatePrice()` method

**Concrete Strategies** (4 types):
- **`StandardPricingStrategy`**: Balanced pricing (weight × $2, distance × $0.5)
- **`ExpressPricingStrategy`**: Premium fast delivery (weight × $3.5, distance × $1.2, +$25 surcharge)
- **`EconomyPricingStrategy`**: Budget pricing (weight × $1.2, distance × $0.3, -15% discount)
- **`OvernightPricingStrategy`**: Overnight premium (weight × $4, distance × $1.5, +$35 fee)

**Code Example**:
```csharp
var basePrice = 50m;
var weight = 15m;    // kg
var distance = 120m; // km

// Create context with initial strategy
var pricing = new DeliveryPricingContext(new StandardPricingStrategy());
var standardPrice = pricing.CalculateDeliveryPrice(basePrice, weight, distance);
// Result: $140.00

// Switch to Express strategy at runtime
pricing.SetStrategy(new ExpressPricingStrategy());
var expressPrice = pricing.CalculateDeliveryPrice(basePrice, weight, distance);
// Result: $269.50

// Switch to Economy strategy
pricing.SetStrategy(new EconomyPricingStrategy());
var economyPrice = pricing.CalculateDeliveryPrice(basePrice, weight, distance);
// Result: $95.20

// Same input, different results based on strategy!
```

**Key Features**:
- Runtime algorithm selection
- Each strategy encapsulates pricing logic
- Context delegates to current strategy
- Strategies are interchangeable

**Benefits**:
- Eliminates conditional statements for algorithm selection
- Easy to add new pricing strategies without modifying existing code
- Strategies can be unit tested independently
- Client can choose optimal algorithm at runtime

---

### **3. State Pattern - Shipment Lifecycle State Machine**

**Purpose**: Allow an object to alter its behavior when its internal state changes

**Context**:
- **`ShipmentContext`**: Maintains current state, delegates behavior to state object, tracks state history

**State Interface**:
- **`IShipmentState`**: Common interface with `Process()`, `MoveToNext()`, `Cancel()` methods

**Concrete States** (5 states):
- **`PendingState`**: Initial state - validates details, assigns resources
  - Can move to: In-Transit
  - Can cancel: Yes → Cancelled
- **`InTransitState`**: Shipment en route
  - Can move to: Out-for-Delivery  
  - Can cancel: Yes (with warning) → Cancelled
- **`OutForDeliveryState`**: With delivery driver
  - Can move to: Delivered
  - Can cancel: No (already with customer)
- **`DeliveredState`**: Terminal success state
  - Can move to: None (terminal)
  - Can cancel: No (use return process)
- **`CancelledState`**: Terminal cancelled state
  - Can move to: None (terminal)
  - Can cancel: Already cancelled

**Code Example**:
```csharp
// Create shipment (starts in Pending state)
var shipment = new ShipmentContext("SHP-001");

shipment.DisplayStatus();
// Current State: Pending
// Allowed Actions: Process, MoveToNext, Cancel

shipment.Process();
// [Pending] Validating shipment details...
// [Pending] Assigning vehicle and driver...
// [Pending] Ready to move to In-Transit

shipment.MoveToNext();
// [State Change] SHP-001: Pending -> In-Transit

shipment.Process();
// [In-Transit] Tracking shipment location...
// [In-Transit] Vehicle en route to destination...

shipment.MoveToNext();
// [State Change] SHP-001: In-Transit -> Out-for-Delivery

shipment.MoveToNext();
// [State Change] SHP-001: Out-for-Delivery -> Delivered

shipment.Process();
// [Delivered] Shipment successfully delivered!
// [Delivered] This is a terminal state

shipment.MoveToNext();
// [Delivered] Already in final state - no next state available

shipment.DisplayHistory();
// [14:30:00] Created in Pending
// [14:30:05] Pending -> In-Transit
// [14:30:10] In-Transit -> Out-for-Delivery
// [14:30:15] Out-for-Delivery -> Delivered
```

**Key Features**:
- State-specific behavior encapsulated in state classes
- State transitions managed by states themselves
- Invalid transitions prevented automatically
- State history tracking
- Context delegates all behavior to current state

**Benefits**:
- Eliminates large conditional statements based on state
- Each state class focuses on single state's behavior (SRP)
- Easy to add new states without modifying existing ones
- State transition logic is localized and clear
- Prevents invalid operations in wrong states

---

## **Architecture Diagram**

```
┌────────────────────────────────────────────────────────────────────┐
│                        Application Layer                           │
│                      (Lab_3.Application)                           │
│                          Program.cs                                │
│  ┌──────────────────────────────────────────────────────────────┐  │
│  │  LAB 3 FOCUS - BEHAVIORAL PATTERNS (3):                      │  │
│  │  • DemonstrateObserver()      - Event notification system    │  │
│  │  • DemonstrateStrategy()      - Dynamic pricing algorithms   │  │
│  │  • DemonstrateState()         - Shipment lifecycle states    │  │
│  │                                                              │  │
│  │  (Foundation: 7 structural + 6 creational patterns)          │  │
│  └──────────────────────────────────────────────────────────────┘  │
└──────────────────────────────┬─────────────────────────────────────┘
                               │ uses
┌──────────────────────────────▼─────────────────────────────────────┐
│                         Services Layer                             │
│                       (Lab_3.Services)                             │
│  ┌─────────────────────────────────────────────────────────────┐   │
│  │ LAB 3 - BEHAVIORAL PATTERNS (Main Focus):                   │   │
│  │                                                             │   │
│  │ • Observer/        - ShipmentTracker + 4 observers          │   │
│  │ • Strategy/        - PricingContext + 4 strategies          │   │
│  │ • State/           - ShipmentContext + 5 states             │   │
│  │                                                             │   │
│  │ LAB 2 - STRUCTURAL PATTERNS (Foundation):                   │   │
│  │ • Adapter, Decorator, Composite, Facade                     │   │
│  │ • Bridge, Flyweight, Proxy                                  │   │
│  │                                                             │   │
│  │ LAB 1 - CREATIONAL PATTERNS (Foundation):                   │   │
│  │ • Singleton, Abstract Factory, Factory Method               │   │
│  │ • Builder, Prototype, Object Pool                           │   │
│  └─────────────────────────────────────────────────────────────┘   │
└──────────────────────────────┬─────────────────────────────────────┘
                               │ implements
┌──────────────────────────────▼─────────────────────────────────────┐
│                          Domain Layer                              │
│                        (Lab_3.Domain)                              │
│  ┌───────────────────┐  ┌─────────────────┐  ┌──────────────────┐  │
│  │    Entities       │  │  Interfaces     │  │      Enums       │  │
│  │  • Vehicle (abs)  │  │                 │  │ • VehicleType    │  │
│  │  • DeliveryTruck  │  │ BEHAVIORAL:     │  │ • VehicleStatus  │  │
│  │  • CargoTruck     │  │ • IShipment     │  │ • DriverLicense  │  │
│  │  • CargoShip      │  │   Observer      │  │   Type           │  │
│  │  • ContainerVessel│  │ • IPricing      │  │ • DriverStatus   │  │
│  │  • CargoPlane     │  │   Strategy      │  │ • PackagePriority│  │
│  │  • Drone          │  │ • IShipment     │  │ • ShipmentStatus │  │
│  │  • Driver         │  │   State         │  │ • RouteType      │  │
│  │  • Package        │  │                 │  │                  │  │
│  │  • Route          │  │ STRUCTURAL:     │  │                  │  │
│  │  • Shipment       │  │ • ITracking     │  │                  │  │
│  │                   │  │   System        │  │                  │  │
│  │                   │  │ • IShipment     │  │                  │  │
│  │                   │  │   Component     │  │                  │  │
│  │                   │  │ • (+ 3 more)    │  │                  │  │
│  │                   │  │                 │  │                  │  │
│  │                   │  │ CREATIONAL:     │  │                  │  │
│  │                   │  │ • ILogistics    │  │                  │  │
│  │                   │  │   Factory       │  │                  │  │
│  │                   │  │ • IVehicle      │  │                  │  │
│  │                   │  │   Factory       │  │                  │  │
│  │                   │  │ • (+ 4 more)    │  │                  │  │
│  └───────────────────┘  └─────────────────┘  └──────────────────┘  │
└────────────────────────────────────────────────────────────────────┘

  Pattern Integration in Logistics System:
  ┌─────────────────────────────────────────────┐
  │  3 Behavioral Patterns (LAB 3 FOCUS)        │
  │  - Observer   - Notification & events       │
  │  - Strategy   - Algorithm selection         │
  │  - State      - Lifecycle management        │
  │                                             │
  │  7 Structural Patterns (Lab 2 Foundation)   │
  │  - Adapter, Decorator, Composite, Facade    │
  │  - Bridge, Flyweight, Proxy                 │
  │                                             │
  │  6 Creational Patterns (Lab 1 Foundation)   │
  │  - Singleton, Abstract Factory, Factory     │
  │    Method, Builder, Prototype, Object Pool  │
  └─────────────────────────────────────────────┘
```

---

## **Conclusions / Results**

In conclusion, **Lab 3** significantly deepened my understanding of **Behavioral Design Patterns** and their practical applications in event-driven systems, dynamic algorithm selection, and stateful behavior management. Implementing **3 distinct behavioral patterns** in a logistics domain demonstrated how these patterns solve complex communication, algorithm selection, and lifecycle challenges while maintaining loose coupling and high flexibility.

### **Behavioral Patterns Deep Dive:**

The **Observer Pattern** demonstrated the power of event-driven architecture by implementing a shipment tracking system where multiple observers (Email, SMS, Dashboard, Logging) react to status changes without tight coupling to the subject (`ShipmentTracker`). This pattern proved invaluable for maintaining separation of concerns - the tracking system doesn't need to know about notification mechanisms, and new observers can be added without modifying existing code. The one-to-many dependency ensures that when one object changes state, all dependents are notified automatically.

**Key Observer Benefits:**
- **Loose Coupling**: Subject and observers are independent; can evolve separately
- **Open/Closed Principle**: New observers can be added without changing subject code
- **Dynamic Subscriptions**: Observers can attach/detach at runtime
- **Real-world Applications**: Event systems, pub-sub architectures, reactive programming

The **Strategy Pattern** showed how to encapsulate a family of interchangeable algorithms (Standard, Express, Economy, Overnight pricing) and make them dynamically selectable at runtime. Instead of using conditional logic (if/switch statements) or creating rigid inheritance hierarchies, Strategy allows the client to choose the appropriate algorithm through composition. This pattern eliminates code duplication and makes adding new pricing strategies trivial.

**Key Strategy Benefits:**
- **Algorithm Flexibility**: Switch pricing strategies without changing client code
- **Eliminates Conditionals**: Replaces if/switch statements with polymorphism
- **Open/Closed Principle**: New strategies can be added without modifying existing code
- **Runtime Selection**: Algorithms can be chosen dynamically based on context
- **Real-world Applications**: Payment processing, sorting algorithms, compression methods

The **State Pattern** elegantly modeled shipment lifecycle management by encapsulating state-specific behavior in separate classes (Pending, InTransit, OutForDelivery, Delivered, Cancelled). Instead of using complex state flags and conditional logic, each state class defines what operations are valid and how transitions occur. This pattern makes state transitions explicit, traceable, and type-safe while maintaining a clear history of state changes.

**Key State Benefits:**
- **Eliminates State Conditionals**: Each state defines its own behavior instead of massive switch statements
- **Explicit State Transitions**: State changes are controlled and validated
- **State History Tracking**: Built-in audit trail of all state changes
- **Enforces Valid Operations**: Invalid operations (e.g., canceling delivered shipment) are prevented
- **Real-world Applications**: Order processing, workflow engines, UI components, protocol implementations

The integrated demonstrations showed how **all 3 behavioral patterns complement each other** in a production-grade logistics system:
- **Observer** handles notifications and monitoring
- **Strategy** provides flexible business logic (pricing, routing, optimization)
- **State** manages lifecycle and workflow transitions

Combined with the 7 structural patterns (Lab 2) and 6 creational patterns (Lab 1), these behavioral patterns complete a comprehensive pattern toolkit that addresses creation, structure, and behavior - the three fundamental aspects of object-oriented design.

**Lab 3 Focus**: Behavioral patterns are about **smart communication** - how objects interact, how responsibilities are distributed, how algorithms are selected, and how state-dependent behavior is managed through proper encapsulation and delegation.

### **Example Outputs:**

**Console Output:**
```
========================================
BEHAVIORAL DESIGN PATTERNS - LAB 3
Logistics System Demo
========================================

========================================
PATTERN 1: OBSERVER PATTERN
Shipment Status Notification System
========================================

Creating ShipmentTracker (Subject)...
Attaching observers:
  - Email Notification Observer (customer@logistics.com)
  - SMS Notification Observer (+1-555-0199)
  - Dashboard Observer (DASHBOARD-MAIN-001)
  - Logging Observer (Audit Trail)

--- Shipment Status Update 1 ---
Updating shipment SHP-12345 to 'Pending' at Warehouse A...

[EMAIL] To: customer@logistics.com
  Subject: Shipment Status Update
  Message: Your shipment SHP-12345 status changed to 'Pending'
  Location: Warehouse A

[SMS] To: +1-555-0199
  Alert: Shipment SHP-12345 is now 'Pending'
  Current location: Warehouse A

[DASHBOARD-MAIN-001] Update #1
  Shipment: SHP-12345
  New Status: Pending
  Location: Warehouse A

[LOG] [15:23:45] AUDIT TRAIL
  Shipment SHP-12345: Unknown -> Pending
  Location: Warehouse A

--- Shipment Status Update 2 ---
Updating shipment SHP-12345 to 'In-Transit' at Distribution Center...

[EMAIL] To: customer@logistics.com
  Subject: Shipment Status Update
  Message: Your shipment SHP-12345 status changed to 'In-Transit'
  Location: Distribution Center

[SMS] To: +1-555-0199
  Alert: Shipment SHP-12345 is now 'In-Transit'
  Current location: Distribution Center

[DASHBOARD-MAIN-001] Update #2
  Shipment: SHP-12345
  New Status: In-Transit
  Location: Distribution Center

[LOG] [15:23:46] AUDIT TRAIL
  Shipment SHP-12345: Pending -> In-Transit
  Location: Distribution Center

--- Shipment Status Update 3 ---
Updating shipment SHP-12345 to 'Delivered' at Customer Address...

[EMAIL] To: customer@logistics.com
  Subject: Shipment Status Update
  Message: Your shipment SHP-12345 status changed to 'Delivered'
  Location: Customer Address

[SMS] To: +1-555-0199
  Alert: Shipment SHP-12345 is now 'Delivered'
  Current location: Customer Address

[DASHBOARD-MAIN-001] Update #3
  Shipment: SHP-12345
  New Status: Delivered
  Location: Customer Address

[LOG] [15:23:47] AUDIT TRAIL
  Shipment SHP-12345: In-Transit -> Delivered
  Location: Customer Address

Observer Pattern Summary:
  - 1 Subject (ShipmentTracker)
  - 4 Observers (Email, SMS, Dashboard, Logging)
  - 3 Status updates triggered 12 notifications (4 per update)
  - Loose coupling: Subject unaware of observer implementations

========================================
PATTERN 2: STRATEGY PATTERN
Dynamic Delivery Pricing Algorithms
========================================

Shipment Details:
  Base Price: $50.00
  Weight: 15.00 kg
  Distance: 120.00 km

--- Strategy 1: Standard Pricing ---
Strategy: Standard Pricing
Description: Balanced pricing for regular deliveries
Calculation:
  Base: $50.00
  Weight cost: 15.00 kg x $2.00 = $30.00
  Distance cost: 120.00 km x $0.50 = $60.00
  Total: $140.00

--- Strategy 2: Express Pricing ---
Switching strategy to: Express Pricing
Description: Premium rates for fast delivery
Calculation:
  Base: $50.00
  Weight cost: 15.00 kg x $3.50 = $52.50
  Distance cost: 120.00 km x $1.20 = $144.00
  Express surcharge: $25.00
  Total: $271.50

--- Strategy 3: Economy Pricing ---
Switching strategy to: Economy Pricing
Description: Budget-friendly flexible delivery
Calculation:
  Base: $50.00
  Weight cost: 15.00 kg x $1.20 = $18.00
  Distance cost: 120.00 km x $0.30 = $36.00
  Subtotal: $104.00
  Economy discount: -15% = -$15.60
  Total: $88.40

--- Strategy 4: Overnight Pricing ---
Switching strategy to: Overnight Pricing
Description: Premium overnight delivery rates
Calculation:
  Base: $50.00
  Weight cost: 15.00 kg x $4.00 = $60.00
  Distance cost: 120.00 km x $1.50 = $180.00
  Overnight fee: $35.00
  Total: $325.00

Strategy Pattern Summary:
  - Same shipment, 4 different pricing algorithms
  - Price range: $88.40 (Economy) to $325.00 (Overnight)
  - Runtime strategy switching enabled
  - No conditional logic needed

========================================
PATTERN 3: STATE PATTERN
Shipment Lifecycle State Machine
========================================

Creating new shipment: SHP-99999

--- Initial State ---
Current State: Pending
Allowed Actions: Process, MoveToNext, Cancel
State History:
  [15:23:48] Created in Pending

--- Processing Pending State ---
  [Pending] Validating shipment details...
  [Pending] Checking inventory availability...
  [Pending] Assigning vehicle and driver...
  [Pending] Preparing shipment documentation...
  [Pending] Ready to move to In-Transit

--- Moving to Next State ---
  [State Change] SHP-99999: Pending -> In-Transit

Current State: In-Transit
Allowed Actions: Process, MoveToNext, Cancel

--- Processing In-Transit State ---
  [In-Transit] Tracking shipment location via GPS...
  [In-Transit] Vehicle en route to destination...
  [In-Transit] Estimated arrival: 2 hours
  [In-Transit] Ready for next transition

--- Moving to Next State ---
  [State Change] SHP-99999: In-Transit -> Out-for-Delivery

Current State: Out-for-Delivery
Allowed Actions: Process, MoveToNext

--- Processing Out-for-Delivery State ---
  [Out-for-Delivery] Driver arrived at delivery zone...
  [Out-for-Delivery] Locating customer address...
  [Out-for-Delivery] Preparing for final delivery...

--- Moving to Next State ---
  [State Change] SHP-99999: Out-for-Delivery -> Delivered

Current State: Delivered
Allowed Actions: Process

--- Processing Delivered State ---
  [Delivered] Shipment successfully delivered!
  [Delivered] Customer signature obtained
  [Delivered] This is a terminal state

--- Attempting Invalid Transition ---
  [Delivered] Already in final state - no next state available

--- Complete State History ---
State History:
  [15:23:48] Created in Pending
  [15:23:49] Pending -> In-Transit
  [15:23:50] In-Transit -> Out-for-Delivery
  [15:23:51] Out-for-Delivery -> Delivered

State Pattern Summary:
  - 5 states implemented (Pending, In-Transit, Out-for-Delivery, Delivered, Cancelled)
  - State transitions: Pending -> In-Transit -> Out-for-Delivery -> Delivered
  - Invalid transitions prevented automatically
  - Full state history tracking

========================================
LAB 3: ALL 3 BEHAVIORAL PATTERNS DEMONSTRATED!

BEHAVIORAL PATTERNS (Lab 3 Focus):
  1. Observer   - Event notification system (4 observers)
  2. Strategy   - Dynamic pricing (4 strategies)
  3. State      - Lifecycle management (5 states)

FOUNDATION PATTERNS (Labs 1 & 2):
  - 7 Structural Patterns (Adapter, Decorator, Composite, Facade, Bridge, Flyweight, Proxy)
  - 6 Creational Patterns (Singleton, Abstract Factory, Factory Method, Builder, Prototype, Object Pool)
========================================

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
cd Lab_3
dotnet restore
dotnet build
dotnet run --project Lab_3.Application
```

**Using Visual Studio/Rider:**
1. Open `Lab_3.sln`
2. Set `Lab_3.Application` as startup project
3. Press F5 or click Run

### **Project Structure:**
```
Lab_3/
├── Lab_3.sln                          # Solution file
├── README.md                          # This file - Behavioral Patterns documentation
├── Lab_3.Domain/                      # Domain layer
│   ├── Entities/                      # Business entities (Vehicle, Driver, Package, etc.)
│   ├── Enums/                         # Enumeration types
│   ├── Interfaces/                    # Pattern contracts
│   │   ├── IShipmentObserver.cs      # Observer pattern interface
│   │   ├── IPricingStrategy.cs       # Strategy pattern interface
│   │   ├── IShipmentState.cs         # State pattern interface
│   │   └── (+ 13 other pattern interfaces)
├── Lab_3.Services/                    # Services layer
│   ├── Observer/                      # LAB 3: Observer (ShipmentTracker + 4 observers)
│   ├── Strategy/                      # LAB 3: Strategy (PricingContext + 4 strategies)
│   ├── State/                         # LAB 3: State (ShipmentContext + 5 states)
│   ├── Adapters/                      # LAB 2: Adapter pattern
│   ├── Decorators/                    # LAB 2: Decorator pattern
│   ├── Composite/                     # LAB 2: Composite pattern
│   ├── Facade/                        # LAB 2: Facade pattern
│   ├── Bridge/                        # LAB 2: Bridge pattern
│   ├── Flyweight/                     # LAB 2: Flyweight pattern
│   ├── Proxy/                         # LAB 2: Proxy pattern
│   ├── Factories/                     # LAB 1: Abstract Factory, Factory Method
│   ├── Builders/                      # LAB 1: Builder pattern
│   ├── Prototypes/                    # LAB 1: Prototype pattern
│   ├── Pools/                         # LAB 1: Object Pool pattern
│   └── Configuration/                 # LAB 1: Singleton pattern
└── Lab_3.Application/                 # Application layer
    └── Program.cs                     # 3 behavioral pattern demonstrations
```

---

## **References**

- **Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides** – Design Patterns: Elements of Reusable Object-Oriented Software (Gang of Four)
- **Robert C. Martin** – Clean Architecture: A Craftsman's Guide to Software Structure and Design
- **Refactoring.Guru** – Behavioral Design Patterns ([refactoring.guru/design-patterns/behavioral-patterns](https://refactoring.guru/design-patterns/behavioral-patterns))
- **Refactoring.Guru** – Observer Pattern ([refactoring.guru/design-patterns/observer](https://refactoring.guru/design-patterns/observer))
- **Refactoring.Guru** – Strategy Pattern ([refactoring.guru/design-patterns/strategy](https://refactoring.guru/design-patterns/strategy))
- **Refactoring.Guru** – State Pattern ([refactoring.guru/design-patterns/state](https://refactoring.guru/design-patterns/state))
- **Microsoft Docs** – C# .NET 9 Documentation ([docs.microsoft.com/en-us/dotnet/](https://docs.microsoft.com/en-us/dotnet/))
- **Microsoft Docs** – Design Patterns in .NET ([docs.microsoft.com/en-us/azure/architecture/patterns/](https://docs.microsoft.com/en-us/azure/architecture/patterns/))
- **Source Making** – Design Patterns ([sourcemaking.com/design_patterns](https://sourcemaking.com/design_patterns))