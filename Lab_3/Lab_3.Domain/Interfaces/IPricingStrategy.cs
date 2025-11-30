namespace Lab_3.Domain.Interfaces;

/// <summary>
///     Strategy interface for delivery pricing calculation
///     Part of the Strategy behavioral pattern
/// </summary>
public interface IPricingStrategy
{
    /// <summary>
    ///     Calculate delivery price based on strategy
    /// </summary>
    /// <param name="basePrice">Base price before strategy application</param>
    /// <param name="weight">Total weight in kg</param>
    /// <param name="distance">Distance in km</param>
    /// <returns>Final calculated price</returns>
    decimal CalculatePrice(decimal basePrice, decimal weight, decimal distance);

    /// <summary>
    /// Gets the strategy name
    /// </summary>
    string StrategyName { get; }

    /// <summary>
    /// Gets the strategy description
    /// </summary>
    string Description { get; }
}
