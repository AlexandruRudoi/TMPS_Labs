using Lab_0.Domain.Entities;
using Lab_0.Domain.Interfaces;

namespace Lab_0.Services;

// Special seasonal discount that can be added without modifying existing code (OCP)
// This demonstrates the Decorator pattern and Open-Closed Principle
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