using Lab_0.Domain.Entities;
using Lab_0.Domain.Interfaces;
using Lab_0.Services;

namespace Lab_0.Application;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to SOLID Principles E-Commerce Demo!");
        Console.WriteLine(new string('=', 60));

        // Create sample products
        var products = new List<Product>
            {
                new(1, "Laptop", 999.99m, "Electronics", 5),
                new(2, "Mouse", 25.50m, "Electronics", 10),
                new(3, "Keyboard", 75.00m, "Electronics", 8),
                new(4, "Monitor", 299.99m, "Electronics", 3),
                new(5, "Book", 19.99m, "Books", 15)
            };

        // Create different types of customers
        var regularCustomer = new Customer(1, "John Doe", "john@email.com", CustomerType.Regular);
        var premiumCustomer = new Customer(2, "Jane Smith", "jane@email.com", CustomerType.Premium);
        var vipCustomer = new Customer(3, "Bob Wilson", "bob@email.com", CustomerType.VIP);

        Console.WriteLine("\nAvailable Products:");
        foreach (var product in products)
        {
            Console.WriteLine($"- {product.Name}: ${product.Price:F2} (Stock: {product.StockQuantity})");
        }

        // Demo 1: Single Responsibility Principle (SRP)
        var cart = new ShoppingCartService();
        DemoSingleResponsibilityPrinciple(products, cart);

        // Demo 2: Open-Closed Principle (OCP)
        DemoOpenClosedPrinciple(vipCustomer);

        // Demo 3: Liskov Substitution Principle (LSP)
        DemoLiskovSubstitutionPrinciple(regularCustomer, premiumCustomer, vipCustomer, cart);

        // Bonus Demo
        DemoBonusOpenClosedExtension(vipCustomer, cart);

        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("SOLID Principles Successfully Demonstrated!");
        Console.WriteLine(new string('=', 60));
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    private static void DemoSingleResponsibilityPrinciple(List<Product> products, ShoppingCartService cart)
    {
        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("DEMO 1: Single Responsibility Principle (SRP)");
        Console.WriteLine(new string('=', 60));

        Console.WriteLine("\nAdding items to cart...");
        cart.AddProduct(products[0]); // Laptop
        cart.AddProduct(products[1]); // Mouse
        cart.AddProduct(products[2]); // Keyboard
        cart.DisplayCart();
    }

    private static void DemoOpenClosedPrinciple(Customer vipCustomer)
    {
        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("DEMO 2: Open-Closed Principle (OCP)");
        Console.WriteLine(new string('=', 60));

        Console.WriteLine("\nTesting different discount calculators:");

        // We can add new discount types without modifying existing code
        var discountCalculators = new Dictionary<string, IDiscountCalculator>
        {
            ["Regular"] = new RegularDiscountCalculator(),
            ["Premium"] = new PremiumDiscountCalculator(),
            ["VIP"] = new VipDiscountCalculator(),
            ["Seasonal VIP"] = new SeasonalDiscountCalculator(new VipDiscountCalculator(), 0.05m)
        };

        var testAmount = 1000m;
        foreach (var (name, calculator) in discountCalculators)
        {
            var discount = calculator.CalculateDiscount(vipCustomer, testAmount);
            Console.WriteLine($"- {name} Calculator: ${discount:F2} discount on ${testAmount:F2}");
        }
    }

    private static void DemoLiskovSubstitutionPrinciple(Customer regularCustomer, Customer premiumCustomer, Customer vipCustomer, ShoppingCartService cart)
    {
        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("DEMO 3: Liskov Substitution Principle (LSP)");
        Console.WriteLine(new string('=', 60));

        Console.WriteLine("\nProcessing orders with different customers and discount calculators:");

        var customers = new[] { regularCustomer, premiumCustomer, vipCustomer };
        var cartItems = cart.GetItems();

        foreach (var customer in customers)
        {
            Console.WriteLine($"\n--- Processing order for {customer.Name} ({customer.Type} customer) ---");

            // LSP in action: We can substitute any discount calculator implementation
            var discountCalculator = DiscountCalculatorFactory.GetDiscountCalculator(customer.Type);
            var orderProcessor = new OrderProcessor(discountCalculator);

            // The OrderProcessor doesn't need to know which specific discount calculator it's using
            // This demonstrates LSP - derived classes are substitutable for their base type
            orderProcessor.ProcessOrder(customer, cartItems);
        }
    }

    private static void DemoBonusOpenClosedExtension(Customer vipCustomer, ShoppingCartService cart)
    {
        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("BONUS: Adding new functionality without modifying existing code!");
        Console.WriteLine(new string('=', 60));

        Console.WriteLine("\nAdding a special Holiday discount (15% extra) for VIP customers:");
        var holidayVipCalculator = new SeasonalDiscountCalculator(new VipDiscountCalculator(), 0.15m);
        var holidayOrderProcessor = new OrderProcessor(holidayVipCalculator);

        Console.WriteLine("\n--- Holiday Special for VIP Customer ---");
        holidayOrderProcessor.ProcessOrder(vipCustomer, cart.GetItems());
    }
}
