using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Strategy;

/// <summary>
///     Overnight pricing strategy for late-night deliveries
///     Concrete strategy implementation
/// </summary>
public class OvernightPricingStrategy : IPricingStrategy
{
    private const decimal WeightMultiplier = 4.0m;
    private const decimal DistanceMultiplier = 1.5m;
    private const decimal OvernightFee = 35.0m;

    public string StrategyName => "Overnight Pricing";
    public string Description => "Premium pricing for overnight deliveries";

    public decimal CalculatePrice(decimal basePrice, decimal weight, decimal distance)
    {
        var weightCost = weight * WeightMultiplier;
        var distanceCost = distance * DistanceMultiplier;
        var total = basePrice + weightCost + distanceCost + OvernightFee;

        Console.WriteLine($"    Base Price: ${basePrice:F2}");
        Console.WriteLine($"    Weight Cost ({weight}kg x ${WeightMultiplier}): ${weightCost:F2}");
        Console.WriteLine($"    Distance Cost ({distance}km x ${DistanceMultiplier}): ${distanceCost:F2}");
        Console.WriteLine($"    Overnight Fee: ${OvernightFee:F2}");

        return total;
    }
}
