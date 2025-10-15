# **SOLID Principles Implementation in E-Commerce System**

### **Course**: Software Design Techniques and Mechanisms

### **Author**: Alexandru Rudoi

---

## **Theory**

**SOLID principles** are a set of five design principles that make software designs more understandable, flexible, and maintainable. This project focuses on implementing the first three principles:

### **S - Single Responsibility Principle (SRP)**
A class should have only **one reason to change**. Each class should be responsible for a single part of the functionality provided by the software.

### **O - Open-Closed Principle (OCP)**  
Software entities should be **open for extension but closed for modification**. You should be able to add new functionality without changing existing code.

### **L - Liskov Substitution Principle (LSP)**
Objects of a superclass should be **replaceable with objects of its subclasses** without breaking the application. Derived classes must be substitutable for their base classes.

### **I - Interface Segregation Principle (ISP)**
Clients should not be forced to depend on interfaces they do not use. It's better to have **many specific interfaces** than one general-purpose interface.

### **D - Dependency Inversion Principle (DIP)**
High-level modules should not depend on low-level modules. Both should depend on **abstractions**. Abstractions should not depend on details, but details should depend on abstractions.

The implementation uses **Clean Architecture** with clear separation of concerns across multiple layers, demonstrating how SOLID principles create maintainable and extensible software systems.

---

## **Objectives**

- Implement **Clean Architecture** with:
  - **Domain Layer**: Core entities and interfaces
  - **Services Layer**: Business logic implementations  
  - **Application Layer**: Program orchestration

- Demonstrate **Single Responsibility Principle** by:
  - Creating classes with single, well-defined purposes
  - Separating concerns across different layers

- Demonstrate **Open-Closed Principle** by:
  - Implementing extensible discount calculation system
  - Adding new functionality without modifying existing code

- Demonstrate **Liskov Substitution Principle** by:
  - Creating interchangeable discount calculator implementations
  - Ensuring derived classes work seamlessly with base interfaces

---

## **Implementation Description**

My e-commerce system consists of three architectural layers:

### **1️⃣ Domain Layer (`Lab_0.Domain`)**

Contains the core business entities and contracts:

#### **Entities:**
- **`Product`**: Represents items in the shop with pricing and stock information
- **`Customer`**: Represents different customer types (Regular, Premium, VIP)

#### **Interfaces:**
- **`IDiscountCalculator`**: Contract for discount calculation strategies
- **`IOrderProcessor`**: Contract for order processing logic
- **`IShoppingCartService`**: Contract for cart management operations

```csharp
public interface IDiscountCalculator
{
    decimal CalculateDiscount(Customer customer, decimal totalAmount);
}
```

---

### **2️⃣ Services Layer (`Lab_0.Services`)**

Contains concrete implementations demonstrating SOLID principles:

#### **Discount Calculators (OCP & LSP):**
- **`RegularDiscountCalculator`**: No discount for regular customers
- **`PremiumDiscountCalculator`**: 10% discount for premium customers  
- **`VipDiscountCalculator`**: 20% discount for VIP customers
- **`SeasonalDiscountCalculator`**: Decorator pattern for additional discounts

#### **Code snippet demonstrating OCP:**
```csharp
// We can add new discount types without modifying existing code
public class SeasonalDiscountCalculator : IDiscountCalculator
{
    private readonly IDiscountCalculator _baseCalculator;
    private readonly decimal _seasonalDiscountPercentage;

    public SeasonalDiscountCalculator(IDiscountCalculator baseCalculator, decimal seasonalDiscountPercentage)
    {
        _baseCalculator = baseCalculator;
        _seasonalDiscountPercentage = seasonalDiscountPercentage;
    }

    public decimal CalculateDiscount(Customer customer, decimal totalAmount)
    {
        var baseDiscount = _baseCalculator.CalculateDiscount(customer, totalAmount);
        var seasonalDiscount = totalAmount * _seasonalDiscountPercentage;
        return baseDiscount + seasonalDiscount;
    }
}
```

#### **Other Services (SRP):**
- **`OrderProcessor`**: Single responsibility for processing orders
- **`ShoppingCartService`**: Single responsibility for cart operations
- **`DiscountCalculatorFactory`**: Single responsibility for creating appropriate calculators

---

### **3️⃣ Application Layer (`Lab_0.Application`)**

Orchestrates the demonstration of all three SOLID principles:

#### **Code snippet demonstrating LSP:**
```csharp
// LSP in action: We can substitute any discount calculator implementation
var discountCalculator = DiscountCalculatorFactory.GetDiscountCalculator(customer.Type);
var orderProcessor = new OrderProcessor(discountCalculator);

// The OrderProcessor doesn't need to know which specific discount calculator it's using
orderProcessor.ProcessOrder(customer, cartItems);
```

The main program demonstrates:
1. **SRP**: Shopping cart functionality with single-purpose classes
2. **OCP**: Different discount calculators and extensibility
3. **LSP**: Interchangeable discount calculator implementations

---

## **SOLID Principles Implementation in Code**

### **Single Responsibility Principle (SRP) Implementation**

Each class in our system has only ONE reason to change:

- **`Product`**: Only changes if product data structure changes
- **`Customer`**: Only changes if customer data structure changes  
- **`ShoppingCartService`**: Only changes if cart logic changes
- **`OrderProcessor`**: Only changes if order processing logic changes
- **`DiscountCalculator` classes**: Only change if their specific discount logic changes
- **`DiscountCalculatorFactory`**: Only changes if new customer types are added

**Example**: The `ShoppingCartService` is responsible ONLY for managing cart operations - adding, removing, displaying items. It doesn't handle discounts, order processing, or customer management.

### **Open-Closed Principle (OCP) Implementation**

Our system is open for extension but closed for modification:

- **Extensible**: We can add new discount calculators without modifying existing code
- **Closed**: Existing discount calculators and order processing logic remain unchanged
- **Decorator Pattern**: `SeasonalDiscountCalculator` extends functionality by wrapping other calculators

**Example**: Adding a new `StudentDiscountCalculator` requires only creating a new class implementing `IDiscountCalculator` - no existing code needs modification.

### **Liskov Substitution Principle (LSP) Implementation**

Any discount calculator can be substituted for another without breaking the code:

- **Interchangeable**: All `IDiscountCalculator` implementations can be used by `OrderProcessor`
- **Consistent Behavior**: Each calculator follows the same contract and behavior expectations
- **Polymorphism**: The `OrderProcessor` works with any discount calculator without knowing the specific type

**Example**: `OrderProcessor` can use `RegularDiscountCalculator`, `PremiumDiscountCalculator`, or `VipDiscountCalculator` interchangeably - the order processing logic remains the same.

---

## **Architecture Diagram**

```
┌─────────────────────────────────────────────────────────────┐
│                    Application Layer                        │
│                  (Lab_0.Application)                        │
│                    Program.cs                               │
└─────────────────────┬───────────────────────────────────────┘
                      │ uses
┌─────────────────────▼───────────────────────────────────────┐
│                    Services Layer                           │
│                   (Lab_0.Services)                          │
│  ┌─────────────────┐ ┌──────────────────┐ ┌──────────────┐  │
│  │DiscountCalc.    │ │ OrderProcessor   │ │ShoppingCart  │  │
│  │ - Regular       │ │                  │ │Service       │  │
│  │ - Premium       │ │                  │ │              │  │
│  │ - VIP           │ │                  │ │              │  │
│  │ - Seasonal      │ │                  │ │              │  │
│  └─────────────────┘ └──────────────────┘ └──────────────┘  │
└─────────────────────┬───────────────────────────────────────┘
                      │ implements
┌─────────────────────▼───────────────────────────────────────┐
│                     Domain Layer                            │
│                   (Lab_0.Domain)                            │
│  ┌─────────────────┐ ┌──────────────────────────────────┐   │
│  │    Entities     │ │           Interfaces             │   │
│  │  - Product      │ │  - IDiscountCalculator           │   │
│  │  - Customer     │ │  - IOrderProcessor               │   │
│  │                 │ │  - IShoppingCartService          │   │
│  └─────────────────┘ └──────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
```

---

## **Conclusions / Results**

In conclusion, this lab significantly enhanced my understanding of SOLID principles and Clean Architecture by implementing a practical e-commerce system. Working with the three core principles - Single Responsibility, Open-Closed, and Liskov Substitution - demonstrated how proper design patterns create maintainable and extensible code. The separation of concerns across Domain, Services, and Application layers showed how Clean Architecture supports SOLID principles in practice.

The implementation revealed how SRP creates focused, maintainable classes, how OCP allows extending functionality without breaking existing code, and how LSP ensures seamless substitutability of implementations. This hands-on approach reinforced theoretical concepts and provided practical experience in designing robust software systems.

### **Example Outputs:**

**Console Output:**
```
Welcome to SOLID Principles E-Commerce Demo!
============================================================

Available Products:
- Laptop: $999.99 (Stock: 5)
- Mouse: $25.50 (Stock: 10)
- Keyboard: $75.00 (Stock: 8)
- Monitor: $299.99 (Stock: 3)
- Book: $19.99 (Stock: 15)

============================================================
DEMO 1: Single Responsibility Principle (SRP)
============================================================

Adding items to cart...
Added Laptop to cart.
Added Mouse to cart.
Added Keyboard to cart.
Shopping Cart:
- Laptop: $999.99
- Mouse: $25.50
- Keyboard: $75.00
Total: $1100.49

============================================================
DEMO 2: Open-Closed Principle (OCP)
============================================================

Testing different discount calculators:
- Regular Calculator: $0.00 discount on $1000.00
- Premium Calculator: $100.00 discount on $1000.00
- VIP Calculator: $200.00 discount on $1000.00
- Seasonal VIP Calculator: $250.00 discount on $1000.00

============================================================
DEMO 3: Liskov Substitution Principle (LSP)
============================================================

Processing orders with different customers and discount calculators:

--- Processing order for John Doe (Regular customer) ---
Order processed for John Doe:
Total: $1100.49
Discount: $0.00
Final Amount: $1100.49

--- Processing order for Jane Smith (Premium customer) ---
Order processed for Jane Smith:
Total: $1100.49
Discount: $110.05
Final Amount: $990.44

--- Processing order for Bob Wilson (VIP customer) ---
Order processed for Bob Wilson:
Total: $1100.49
Discount: $220.10
Final Amount: $880.39

============================================================
BONUS: Adding new functionality without modifying existing code!
============================================================

Adding a special Holiday discount (15% extra) for VIP customers:

--- Holiday Special for VIP Customer ---
Order processed for Bob Wilson:
Total: $1100.49
Discount: $385.17
Final Amount: $715.32

============================================================
SOLID Principles Successfully Demonstrated!
============================================================

Press any key to exit...
```

---

## **References**

- **Robert C. Martin** – Clean Architecture: A Craftsman's Guide to Software Structure and Design
- SOLID Principles – Software Design Patterns and Best Practices
- C# .NET 9 Documentation ([docs.microsoft.com](https://docs.microsoft.com/en-us/dotnet/))