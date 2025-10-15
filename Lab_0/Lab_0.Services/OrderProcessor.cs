using Lab_0.Domain.Entities;
using Lab_0.Domain.Interfaces;

namespace Lab_0.Services;

// SRP: This class has only one responsibility - processing orders
public class OrderProcessor : IOrderProcessor
{
    private readonly IDiscountCalculator _discountCalculator;

    public OrderProcessor(IDiscountCalculator discountCalculator)
    {
        _discountCalculator = discountCalculator;
    }

    public bool ProcessOrder(Customer customer, List<Product> products)
    {
        if (!products.Any())
        {
            Console.WriteLine("Cannot process order: No products selected.");
            return false;
        }

        // Check stock availability
        foreach (var product in products)
        {
            if (product.StockQuantity <= 0)
            {
                Console.WriteLine($"Cannot process order: {product.Name} is out of stock.");
                return false;
            }
        }

        // Calculate total
        decimal totalAmount = products.Sum(p => p.Price);
        
        // Apply discount using the injected calculator (LSP in action!)
        decimal discount = _discountCalculator.CalculateDiscount(customer, totalAmount);
        decimal finalAmount = totalAmount - discount;

        Console.WriteLine($"Order processed for {customer.Name}:");
        Console.WriteLine($"Total: ${totalAmount:F2}");
        Console.WriteLine($"Discount: ${discount:F2}");
        Console.WriteLine($"Final Amount: ${finalAmount:F2}");

        return true;
    }
}