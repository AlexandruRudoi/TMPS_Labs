using Lab_0.Domain.Entities;
using Lab_0.Domain.Interfaces;

namespace Lab_0.Services;

// SRP: This class has only one responsibility - managing shopping cart operations
public class ShoppingCartService : IShoppingCartService
{
    private readonly List<Product> _items = new();

    public void AddProduct(Product product)
    {
        if (product.StockQuantity > 0)
        {
            _items.Add(product);
            Console.WriteLine($"Added {product.Name} to cart.");
        }
        else
        {
            Console.WriteLine($"Cannot add {product.Name}: Out of stock.");
        }
    }

    public void RemoveProduct(int productId)
    {
        var product = _items.FirstOrDefault(p => p.Id == productId);
        if (product != null)
        {
            _items.Remove(product);
            Console.WriteLine($"Removed {product.Name} from cart.");
        }
    }

    public List<Product> GetItems()
    {
        return new List<Product>(_items);
    }

    public decimal GetTotal()
    {
        return _items.Sum(p => p.Price);
    }

    public void ClearCart()
    {
        _items.Clear();
        Console.WriteLine("Cart cleared.");
    }

    public void DisplayCart()
    {
        if (!_items.Any())
        {
            Console.WriteLine("Your cart is empty.");
            return;
        }

        Console.WriteLine("Shopping Cart:");
        foreach (var item in _items)
        {
            Console.WriteLine($"- {item.Name}: ${item.Price:F2}");
        }
        Console.WriteLine($"Total: ${GetTotal():F2}");
    }
}