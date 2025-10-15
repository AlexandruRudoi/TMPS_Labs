using Lab_0.Domain.Entities;

namespace Lab_0.Domain.Interfaces;

// SRP: This interface has only one responsibility - calculating discounts
public interface IDiscountCalculator
{
    decimal CalculateDiscount(Customer customer, decimal totalAmount);
}