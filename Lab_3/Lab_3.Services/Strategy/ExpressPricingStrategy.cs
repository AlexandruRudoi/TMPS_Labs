using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Strategy;

/// <summary>
///     Express pricing strategy with premium rates for faster delivery
///     Concrete strategy implementation
/// </summary>
public class ExpressPricingStrategy : IPricingStrategy
{
    private const decimal WeightMultiplier = 3.5m;
    private const decimal DistanceMultiplier = 1.2m;
    private const decimal ExpressSurcharge = 25.0m;

    public string StrategyName => "Express Pricing";
    public string Description => "Premium pricing for same-day/next-day delivery";

    public decimal CalculatePrice(decimal basePrice, decimal weight, decimal distance)
    {
        var weightCost = weight * WeightMultiplier;
        var distanceCost = distance * DistanceMultiplier;
        var total = basePrice + weightCost + distanceCost + ExpressSurcharge;

        Console.WriteLine($"    Base Price: ${basePrice:F2}");
        Console.WriteLine($"    Weight Cost ({weight}kg x ${WeightMultiplier}): ${weightCost:F2}");
        Console.WriteLine($"    Distance Cost ({distance}km x ${DistanceMultiplier}): ${distanceCost:F2}");
        Console.WriteLine($"    Express Surcharge: ${ExpressSurcharge:F2}");

        return total;
    }
}
