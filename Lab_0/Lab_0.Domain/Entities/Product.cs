namespace Lab_0.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    
    public Product(int id, string name, decimal price, string category, int stockQuantity)
    {
        Id = id;
        Name = name;
        Price = price;
        Category = category;
        StockQuantity = stockQuantity;
    }
}