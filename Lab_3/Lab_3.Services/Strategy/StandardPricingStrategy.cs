using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Strategy;

/// <summary>
///     Standard pricing strategy with balanced rates
///     Concrete strategy implementation
/// </summary>
public class StandardPricingStrategy : IPricingStrategy
{
    private const decimal WeightMultiplier = 2.0m;
    private const decimal DistanceMultiplier = 0.5m;

    public string StrategyName => "Standard Pricing";
    public string Description => "Balanced pricing for regular deliveries";

    public decimal CalculatePrice(decimal basePrice, decimal weight, decimal distance)
    {
        var weightCost = weight * WeightMultiplier;
        var distanceCost = distance * DistanceMultiplier;
        var total = basePrice + weightCost + distanceCost;

        Console.WriteLine($"    Base Price: ${basePrice:F2}");
        Console.WriteLine($"    Weight Cost ({weight}kg x ${WeightMultiplier}): ${weightCost:F2}");
        Console.WriteLine($"    Distance Cost ({distance}km x ${DistanceMultiplier}): ${distanceCost:F2}");

        return total;
    }
}
