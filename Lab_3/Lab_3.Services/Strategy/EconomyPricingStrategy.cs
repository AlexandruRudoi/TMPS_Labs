using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Strategy;

/// <summary>
///     Economy pricing strategy with reduced rates for flexible delivery
///     Concrete strategy implementation
/// </summary>
public class EconomyPricingStrategy : IPricingStrategy
{
    private const decimal WeightMultiplier = 1.2m;
    private const decimal DistanceMultiplier = 0.3m;
    private const decimal EconomyDiscount = 0.15m; // 15% discount

    public string StrategyName => "Economy Pricing";
    public string Description => "Budget-friendly pricing for flexible delivery schedules";

    public decimal CalculatePrice(decimal basePrice, decimal weight, decimal distance)
    {
        var weightCost = weight * WeightMultiplier;
        var distanceCost = distance * DistanceMultiplier;
        var subtotal = basePrice + weightCost + distanceCost;
        var discount = subtotal * EconomyDiscount;
        var total = subtotal - discount;

        Console.WriteLine($"    Base Price: ${basePrice:F2}");
        Console.WriteLine($"    Weight Cost ({weight}kg x ${WeightMultiplier}): ${weightCost:F2}");
        Console.WriteLine($"    Distance Cost ({distance}km x ${DistanceMultiplier}): ${distanceCost:F2}");
        Console.WriteLine($"    Subtotal: ${subtotal:F2}");
        Console.WriteLine($"    Economy Discount (15%): -${discount:F2}");

        return total;
    }
}
