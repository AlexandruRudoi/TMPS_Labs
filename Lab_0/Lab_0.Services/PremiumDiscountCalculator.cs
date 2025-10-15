using Lab_0.Domain.Entities;
using Lab_0.Domain.Interfaces;

namespace Lab_0.Services;

// Premium customers get 10% discount
public class PremiumDiscountCalculator : IDiscountCalculator
{
    public decimal CalculateDiscount(Customer customer, decimal totalAmount)
    {
        return totalAmount * 0.10m; // 10% discount
    }
}