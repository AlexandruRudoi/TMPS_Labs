using Lab_0.Domain.Entities;

namespace Lab_0.Domain.Interfaces;

// SRP: This interface has only one responsibility - processing orders
public interface IOrderProcessor
{
    bool ProcessOrder(Customer customer, List<Product> products);
}