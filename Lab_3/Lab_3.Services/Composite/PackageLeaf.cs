using Lab_3.Domain.Entities;
using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Composite;

/// <summary>
///     Leaf component for Composite pattern
///     Represents a single package (cannot contain other packages)
/// </summary>
public class PackageLeaf : IPackageComponent
{
    private readonly Package _package;

    /// <summary>
    ///     Initializes a new instance of the PackageLeaf
    /// </summary>
    /// <param name="package">The package to wrap</param>
    public PackageLeaf(Package package)
    {
        _package = package;
    }

    /// <inheritdoc />
    public string Id => _package.Id;

    /// <inheritdoc />
    public string Description => _package.Description;

    /// <inheritdoc />
    public decimal GetTotalWeight()
    {
        return _package.Weight;
    }

    /// <inheritdoc />
    public int GetPackageCount()
    {
        return 1;
    }

    /// <inheritdoc />
    public void Display(int indent = 0)
    {
        var indentation = new string(' ', indent * 2);
        Console.WriteLine($"{indentation}   Package: {_package.Id}");
        Console.WriteLine($"{indentation}   Description: {_package.Description}");
        Console.WriteLine($"{indentation}   Weight: {_package.Weight}kg");
        Console.WriteLine($"{indentation}   Destination: {_package.DestinationAddress}");
    }

    /// <inheritdoc />
    public List<Package> GetAllPackages()
    {
        return new List<Package> { _package };
    }
}
