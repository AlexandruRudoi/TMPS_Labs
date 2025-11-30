# Software Design Techniques and Mechanisms

## Repository Overview

This repository contains laboratory work for the **Software Design Techniques and Mechanisms** course, demonstrating the implementation of various **Design Patterns** in a real-world logistics/dispatch system using **Clean Architecture** principles with **.NET 9.0** and **C#**.

Each lab builds upon the previous one, progressively adding more design patterns while maintaining clean separation of concerns across Domain, Services, and Application layers.

---

## Laboratory Work Summary

| Lab | Focus | Patterns Implemented | Key Concepts | Status |
|-----|-------|---------------------|--------------|---------|
| **Lab 0** | SOLID Principles & DI | N/A - Foundation | Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion, Dependency Injection | ✅ Complete |
| **Lab 1** | Creational Patterns | **6 Patterns:** Singleton, Abstract Factory, Factory Method, Builder, Prototype, Object Pool | Object creation, Resource management, Abstraction of instantiation | ✅ Complete |
| **Lab 2** | Structural Patterns | **7 Patterns:** Adapter, Decorator, Composite, Facade, Bridge, Flyweight, Proxy | Object composition, Flexible relationships, Interface adaptation | ✅ Complete |
| **Lab 3** | Behavioral Patterns | **3 Patterns:** Observer, Strategy, State | Object communication, Algorithm encapsulation, State management | ✅ Complete |

---

## 📖 Detailed Lab Descriptions

### **Lab 0: SOLID Principles & Dependency Injection**
**Folder:** `Lab_0/`

**Objective:** Establish a foundation by implementing SOLID principles and dependency injection in a simple order processing system.

**Topics Covered:**
- **Single Responsibility Principle (SRP)** - Each class has one reason to change
- **Open/Closed Principle (OCP)** - Open for extension, closed for modification
- **Liskov Substitution Principle (LSP)** - Derived classes must be substitutable for base classes
- **Interface Segregation Principle (ISP)** - Clients shouldn't depend on interfaces they don't use
- **Dependency Inversion Principle (DIP)** - Depend on abstractions, not concretions
- **Dependency Injection** - Constructor injection for loose coupling

**Key Components:**
- Customer and Product entities
- Discount calculators (Regular, Premium, Seasonal, VIP)
- Order processor with discount calculation factory
- Shopping cart service

---

### **Lab 1: Creational Design Patterns**
**Folder:** `Lab_1/`  
**Documentation:** [Lab_1/README.md](Lab_1/README.md)

**Objective:** Implement 6 creational design patterns to manage object creation in a logistics system.

**Patterns Implemented:**
1. **Singleton** - Centralized configuration management (thread-safe using `Lazy<T>`)
2. **Abstract Factory** - Region-specific logistics factories (Urban, Rural, International)
3. **Factory Method** - Simple vehicle creation without exposing concrete classes
4. **Builder** - Step-by-step complex shipment construction with validation
5. **Prototype** - Route template cloning for efficient object creation
6. **Object Pool** - Vehicle and driver resource pooling for performance

**Architecture:**
- **Domain Layer:** Entities (Vehicle, Driver, Package, Route, Shipment), Interfaces, Enums
- **Services Layer:** Pattern implementations (Factories, Builders, Pools, Prototypes, Configuration)
- **Application Layer:** Pattern demonstrations with console output

**Highlights:**
- Clean Architecture with 3 layers
- Thread-safe implementations
- Comprehensive demonstrations showing pattern interactions

---

### **Lab 2: Structural Design Patterns**
**Folder:** `Lab_2/`  
**Documentation:** [Lab_2/README.md](Lab_2/README.md)

**Objective:** Implement 7 structural design patterns to create flexible object relationships and compositions.

**Patterns Implemented:**
1. **Adapter** - Unified interface for third-party tracking systems (GPS, RFID, Barcode)
2. **Decorator** - Dynamic shipment enhancements (Insurance, Priority, Signature, Temperature Control, Fragile)
3. **Composite** - Hierarchical package management (Packages and Containers in tree structures)
4. **Facade** - Simplified logistics coordination interface (LogisticsFacade)
5. **Bridge** - Separation of vehicle operations from control systems (Manual, Automatic, Autonomous, Remote)
6. **Flyweight** - Memory optimization through shared package type data
7. **Proxy** - Access control and optimization (Lazy loading, Protection, Caching proxies)

**Architecture:**
- Builds on Lab 1 foundation (all 6 creational patterns retained)
- Added structural pattern implementations
- Integrated scenario demonstrating all 13 patterns working together

**Highlights:**
- Smart object composition without tight coupling
- Memory optimization techniques
- Access control and lazy loading strategies
- Comprehensive integration demonstration

---

### **Lab 3: Behavioral Design Patterns**
**Folder:** `Lab_3/`  
**Documentation:** [Lab_3/README.md](Lab_3/README.md)

**Objective:** Implement 3 behavioral design patterns to manage object communication, algorithm selection, and state-dependent behavior.

**Patterns Implemented:**
1. **Observer** - Event-driven shipment status notification system
   - 1 Subject (ShipmentTracker)
   - 4 Observers (Email, SMS, Dashboard, Logging)
   - One-to-many dependency for automatic notifications

2. **Strategy** - Dynamic delivery pricing with interchangeable algorithms
   - 4 Pricing strategies (Standard, Express, Economy, Overnight)
   - Runtime algorithm switching
   - Eliminates conditional logic

3. **State** - Shipment lifecycle state machine
   - 5 States (Pending, In-Transit, Out-for-Delivery, Delivered, Cancelled)
   - State-specific behavior encapsulation
   - Automatic state transition validation
   - Complete state history tracking

**Architecture:**
- Builds on Labs 1 & 2 (13 patterns as foundation)
- **Domain Layer:** Behavioral pattern interfaces (IShipmentObserver, IPricingStrategy, IShipmentState, IShipmentContext)
- **Services Layer:** Concrete implementations for all 3 patterns
- **Application Layer:** Focused demonstrations of behavioral patterns

**Highlights:**
- Event-driven architecture with Observer
- Flexible algorithm selection with Strategy
- Robust state management with State pattern
- Clean Architecture maintained across all layers
- No circular dependencies (solved with interface abstraction)

---

## Architecture Overview

All labs follow **Clean Architecture** principles:

```
┌─────────────────────────────────────────┐
│     Application Layer                   │
│     - Pattern demonstrations            │
│     - User interface / Console output   │
└──────────────┬──────────────────────────┘
               │ uses
┌──────────────▼──────────────────────────┐
│     Services Layer                      │
│     - Pattern implementations           │
│     - Business logic                    │
│     - Concrete classes                  │
└──────────────┬──────────────────────────┘
               │ implements
┌──────────────▼──────────────────────────┐
│     Domain Layer                        │
│     - Entities                          │
│     - Interfaces (contracts)            │
│     - Enums                             │
│     - No dependencies                   │
└─────────────────────────────────────────┘
```

**Benefits:**
- Clear separation of concerns
- Domain layer is framework-independent
- Easy to test and maintain
- Scalable and extensible

---

## Getting Started

### **Prerequisites:**
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- IDE: Visual Studio 2022, JetBrains Rider, or VS Code with C# extension

### **Build and Run:**

```bash
# Navigate to specific lab folder
cd Lab_1  # or Lab_2, Lab_3

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run --project Lab_X.Application
```

Replace `X` with the lab number (1, 2, or 3).

---

## Pattern Count Summary

| Pattern Category | Lab 1 | Lab 2 | Lab 3 | **Total** |
|------------------|-------|-------|-------|-----------|
| **Creational**   | 6     | 6     | 6     | **6**     |
| **Structural**   | -     | 7     | 7     | **7**     |
| **Behavioral**   | -     | -     | 3     | **3**     |
| **TOTAL**        | 6     | 13    | 16    | **16**    |

*Note: Each lab builds on previous labs, so Lab 3 contains all 16 patterns working together.*

---

## Repository Structure

```
TMPS_Labs/
├── Lab_0/                      # SOLID Principles & DI
│   ├── Lab_0.Application/      # Console application
│   ├── Lab_0.Domain/           # Entities and interfaces
│   ├── Lab_0.Services/         # Business logic
│   └── README.md               # Lab 0 documentation
├── Lab_1/                      # Creational Patterns
│   ├── Lab_1.Application/      # Pattern demonstrations
│   ├── Lab_1.Domain/           # Domain entities and contracts
│   ├── Lab_1.Services/         # Pattern implementations
│   └── README.md               # Lab 1 documentation
├── Lab_2/                      # Structural Patterns
│   ├── Lab_2.Application/      # Pattern demonstrations
│   ├── Lab_2.Domain/           # Domain layer
│   ├── Lab_2.Services/         # Pattern implementations
│   └── README.md               # Lab 2 documentation
├── Lab_3/                      # Behavioral Patterns
│   ├── Lab_3.Application/      # Pattern demonstrations
│   ├── Lab_3.Domain/           # Domain layer with interfaces
│   ├── Lab_3.Services/         # Pattern implementations
│   ├── README.md               # Lab 3 main documentation
│   └── BEHAVIORAL_PATTERNS.md  # Detailed pattern guide
└── README.md                   # This file
```

---

## Learning Outcomes

By completing these labs, you will understand:

✅ **SOLID Principles** and their practical application  
✅ **Dependency Injection** and Inversion of Control  
✅ **Clean Architecture** and layered design  
✅ **16 Gang of Four Design Patterns** with real-world implementations  
✅ How patterns work together in complex systems  
✅ When and why to use specific patterns  
✅ Trade-offs and best practices for each pattern  

---

## References

- **Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides** - Design Patterns: Elements of Reusable Object-Oriented Software (Gang of Four)
- **Robert C. Martin** - Clean Architecture: A Craftsman's Guide to Software Structure and Design
- **Robert C. Martin** - Clean Code: A Handbook of Agile Software Craftsmanship
- **Refactoring.Guru** - Design Patterns (https://refactoring.guru/design-patterns)
- **Microsoft Docs** - .NET Documentation (https://docs.microsoft.com/en-us/dotnet/)
- **Source Making** - Design Patterns (https://sourcemaking.com/design_patterns)

---

## Author

**Alexandru Rudoi**  
Student, Technical University of Moldova  
Course: Software Design Techniques and Mechanisms (TMPS)

---

## License

This project is created for educational purposes as part of university coursework.