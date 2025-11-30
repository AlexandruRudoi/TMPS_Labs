using Lab_3.Domain.Entities;
using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Composite;

/// <summary>
///     Composite component for Composite pattern
///     Represents a container that can hold packages or other containers
/// </summary>
public class PackageContainer : IPackageComponent
{
    private readonly List<IPackageComponent> _children;
    private readonly string _containerType;

    /// <summary>
    ///     Initializes a new instance of the PackageContainer
    /// </summary>
    /// <param name="id">Container identifier</param>
    /// <param name="description">Container description</param>
    /// <param name="containerType">Type of container (Pallet, Box, Crate, etc.)</param>
    public PackageContainer(string id, string description, string containerType = "Container")
    {
        Id = id;
        Description = description;
        _containerType = containerType;
        _children = new List<IPackageComponent>();
    }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public string Description { get; }

    /// <summary>
    ///     Gets the list of child components
    /// </summary>
    public IReadOnlyList<IPackageComponent> Children => _children.AsReadOnly();

    /// <summary>
    ///     Adds a package or container to this container
    /// </summary>
    /// <param name="component">Package or container to add</param>
    public void Add(IPackageComponent component)
    {
        _children.Add(component);
    }

    /// <summary>
    ///     Removes a package or container from this container
    /// </summary>
    /// <param name="component">Package or container to remove</param>
    public void Remove(IPackageComponent component)
    {
        _children.Remove(component);
    }

    /// <summary>
    ///     Gets a child component by index
    /// </summary>
    public IPackageComponent GetChild(int index)
    {
        return _children[index];
    }

    /// <inheritdoc />
    public decimal GetTotalWeight()
    {
        // Sum of all children weights
        return _children.Sum(child => child.GetTotalWeight());
    }

    /// <inheritdoc />
    public int GetPackageCount()
    {
        // Sum of all children package counts
        return _children.Sum(child => child.GetPackageCount());
    }

    /// <inheritdoc />
    public void Display(int indent = 0)
    {
        var indentation = new string(' ', indent * 2);
        Console.WriteLine($"{indentation}   {_containerType}: {Id}");
        Console.WriteLine($"{indentation}   Description: {Description}");
        Console.WriteLine($"{indentation}   Total Weight: {GetTotalWeight()}kg");
        Console.WriteLine($"{indentation}   Package Count: {GetPackageCount()}");
        Console.WriteLine($"{indentation}   Contains:");
        
        foreach (var child in _children)
        {
            child.Display(indent + 1);
        }
    }

    /// <inheritdoc />
    public List<Package> GetAllPackages()
    {
        var packages = new List<Package>();
        
        foreach (var child in _children)
        {
            packages.AddRange(child.GetAllPackages());
        }
        
        return packages;
    }
}
