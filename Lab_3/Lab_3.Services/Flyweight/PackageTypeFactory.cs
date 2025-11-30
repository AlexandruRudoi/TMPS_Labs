namespace Lab_3.Services.Flyweight;

/// <summary>
///     Flyweight Factory for managing shared PackageType instances
///     Ensures only one instance per package category exists
/// </summary>
public class PackageTypeFactory
{
    private readonly Dictionary<string, PackageType> _packageTypes = new();
    private static readonly object _lock = new();

    /// <summary>
    ///     Gets or creates a PackageType flyweight
    /// </summary>
    public PackageType GetPackageType(string category)
    {
        lock (_lock)
        {
            if (!_packageTypes.ContainsKey(category))
            {
                _packageTypes[category] = CreatePackageType(category);
                Console.WriteLine($"   Created new PackageType flyweight: {category}");
            }
            else
            {
                Console.WriteLine($"   Reusing existing PackageType flyweight: {category}");
            }

            return _packageTypes[category];
        }
    }

    /// <summary>
    ///     Gets total number of flyweight instances
    /// </summary>
    public int GetFlyweightCount() => _packageTypes.Count;

    /// <summary>
    ///     Gets all registered package type categories
    /// </summary>
    public IEnumerable<string> GetCategories() => _packageTypes.Keys;

    /// <summary>
    ///     Creates predefined package types
    /// </summary>
    private PackageType CreatePackageType(string category)
    {
        return category.ToLower() switch
        {
            "electronics" => new PackageType(
                "Electronics",
                "Keep dry, avoid magnetic fields, handle with care",
                "Anti-static bubble wrap",
                isFragile: true,
                icon: "[01]"
            ),
            "books" => new PackageType(
                "Books",
                "Keep dry, stack carefully",
                "Cardboard box",
                isFragile: false,
                icon: "[02]"
            ),
            "clothing" => new PackageType(
                "Clothing",
                "Keep dry and clean",
                "Plastic bag",
                isFragile: false,
                icon: "[03]"
            ),
            "food" => new PackageType(
                "Food",
                "Keep refrigerated, check expiry",
                "Insulated container",
                isFragile: false,
                icon: "[04]"
            ),
            "pharmaceuticals" => new PackageType(
                "Pharmaceuticals",
                "Temperature controlled, secure handling",
                "Medical-grade container",
                isFragile: true,
                icon: "[05]"
            ),
            "glass" => new PackageType(
                "Glass/Fragile",
                "FRAGILE - Handle with extreme care",
                "Heavy-duty bubble wrap with corner protection",
                isFragile: true,
                icon: "[06]"
            ),
            "documents" => new PackageType(
                "Documents",
                "Keep dry, confidential handling",
                "Waterproof envelope",
                isFragile: false,
                icon: "[07]"
            ),
            "furniture" => new PackageType(
                "Furniture",
                "Heavy item, use proper lifting equipment",
                "Foam padding and shrink wrap",
                isFragile: false,
                icon: "[08]"
            ),
            _ => new PackageType(
                "General",
                "Standard handling procedures",
                "Standard cardboard box",
                isFragile: false,
                icon: "[09]"
            )
        };
    }
}
