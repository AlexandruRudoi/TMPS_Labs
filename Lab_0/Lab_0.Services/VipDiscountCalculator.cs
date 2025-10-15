using Lab_0.Domain.Entities;
using Lab_0.Domain.Interfaces;

namespace Lab_0.Services;

// VIP customers get 20% discount
public class VipDiscountCalculator : IDiscountCalculator
{
    public decimal CalculateDiscount(Customer customer, decimal totalAmount)
    {
        return totalAmount * 0.20m; // 20% discount
    }
}