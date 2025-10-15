using Lab_0.Domain.Entities;
using Lab_0.Domain.Interfaces;

namespace Lab_0.Services;

// OCP: We can extend discount calculation by creating new implementations without modifying existing code
// LSP: All implementations can be substituted for the interface

// Base discount calculator for regular customers
public class RegularDiscountCalculator : IDiscountCalculator
{
    public virtual decimal CalculateDiscount(Customer customer, decimal totalAmount)
    {
        // Regular customers get no discount
        return 0;
    }
}