namespace Lab_3.Services.Flyweight;

/// <summary>
///     Context class that uses Flyweight
///     Contains extrinsic state (unique to each package) and reference to shared flyweight
/// </summary>
public class FlyweightPackage
{
    // Extrinsic state (unique to this package)
    private readonly string _id;

    // Intrinsic state (shared via flyweight)
    private readonly PackageType _packageType;
    private readonly string _recipient;
    private readonly string _sender;
    private readonly string _trackingNumber;
    private readonly decimal _weight;

    public FlyweightPackage(
        string id,
        decimal weight,
        string sender,
        string recipient,
        string trackingNumber,
        PackageType packageType)
    {
        _id = id;
        _weight = weight;
        _sender = sender;
        _recipient = recipient;
        _trackingNumber = trackingNumber;
        _packageType = packageType;
    }

    public void Display()
    {
        Console.WriteLine($"\n  Package {_id}");
        Console.WriteLine($"   Tracking: {_trackingNumber}");
        Console.WriteLine($"   Weight: {_weight}kg");
        Console.WriteLine($"   From: {_sender}");
        Console.WriteLine($"   To: {_recipient}");
        Console.WriteLine("   Type Information (Shared):");
        _packageType.Display();
    }

    public string GetId()
    {
        return _id;
    }

    public decimal GetWeight()
    {
        return _weight;
    }

    public string GetCategory()
    {
        return _packageType.Category;
    }
}