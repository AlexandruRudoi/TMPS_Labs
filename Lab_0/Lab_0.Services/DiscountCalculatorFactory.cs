using Lab_0.Domain.Entities;
using Lab_0.Domain.Interfaces;

namespace Lab_0.Services;

// SRP: This class has only one responsibility - managing the discount calculation strategy
public class DiscountCalculatorFactory
{
    public static IDiscountCalculator GetDiscountCalculator(CustomerType customerType)
    {
        return customerType switch
        {
            CustomerType.Regular => new RegularDiscountCalculator(),
            CustomerType.Premium => new PremiumDiscountCalculator(),
            CustomerType.VIP => new VipDiscountCalculator(),
            _ => new RegularDiscountCalculator()
        };
    }
}