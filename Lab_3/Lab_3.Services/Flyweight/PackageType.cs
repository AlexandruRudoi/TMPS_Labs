namespace Lab_3.Services.Flyweight;

/// <summary>
///     Flyweight for shared package type data
///     Stores intrinsic state (shared among many packages)
/// </summary>
public class PackageType
{
    public PackageType(string category, string handlingInstructions, string packagingMaterial, bool isFragile,
        string icon)
    {
        Category = category;
        HandlingInstructions = handlingInstructions;
        PackagingMaterial = packagingMaterial;
        IsFragile = isFragile;
        Icon = icon;
    }

    /// <summary>
    ///     Category of the package
    /// </summary>
    public string Category { get; }

    /// <summary>
    ///     Handling instructions for this type
    /// </summary>
    public string HandlingInstructions { get; }

    /// <summary>
    ///     Standard packaging material
    /// </summary>
    public string PackagingMaterial { get; }

    /// <summary>
    ///     Whether this type is fragile
    /// </summary>
    public bool IsFragile { get; }

    /// <summary>
    ///     Icon/symbol for this package type
    /// </summary>
    public string Icon { get; }

    public void Display()
    {
        Console.WriteLine($"   {Icon} Category: {Category}");
        Console.WriteLine($"      Handling: {HandlingInstructions}");
        Console.WriteLine($"      Material: {PackagingMaterial}");
        Console.WriteLine($"      Fragile: {IsFragile}");
    }
}