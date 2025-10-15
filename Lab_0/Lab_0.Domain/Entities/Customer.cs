namespace Lab_0.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public CustomerType Type { get; set; }
    
    public Customer(int id, string name, string email, CustomerType type)
    {
        Id = id;
        Name = name;
        Email = email;
        Type = type;
    }
}

public enum CustomerType
{
    Regular,
    Premium,
    VIP
}