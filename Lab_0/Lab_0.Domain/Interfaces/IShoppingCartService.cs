using Lab_0.Domain.Entities;

namespace Lab_0.Domain.Interfaces;

// SRP: This interface has only one responsibility - defining shopping cart operations
public interface IShoppingCartService
{
    void AddProduct(Product product);
    void RemoveProduct(int productId);
    List<Product> GetItems();
    decimal GetTotal();
    void ClearCart();
    void DisplayCart();
}