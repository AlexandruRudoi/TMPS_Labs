using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Strategy;

/// <summary>
///     Context class that uses a pricing strategy
///     Part of the Strategy behavioral pattern
/// </summary>
public class DeliveryPricingContext
{
    private IPricingStrategy _pricingStrategy;

    public DeliveryPricingContext(IPricingStrategy pricingStrategy)
    {
        _pricingStrategy = pricingStrategy;
    }

    /// <summary>
    ///     Change the pricing strategy at runtime
    /// </summary>
    public void SetStrategy(IPricingStrategy strategy)
    {
        _pricingStrategy = strategy;
        Console.WriteLine($"\n[Context] Pricing strategy changed to: {strategy.StrategyName}");
    }

    /// <summary>
    ///     Calculate price using the current strategy
    /// </summary>
    public decimal CalculateDeliveryPrice(decimal basePrice, decimal weight, decimal distance)
    {
        Console.WriteLine($"\n[Context] Using {_pricingStrategy.StrategyName}");
        Console.WriteLine($"  Description: {_pricingStrategy.Description}");
        
        var price = _pricingStrategy.CalculatePrice(basePrice, weight, distance);
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  TOTAL PRICE: ${price:F2}");
        Console.ResetColor();

        return price;
    }

    /// <summary>
    ///     Get current strategy name
    /// </summary>
    public string GetCurrentStrategy() => _pricingStrategy.StrategyName;
}
