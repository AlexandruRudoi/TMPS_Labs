namespace Lab_2.Domain.Interfaces;

/// <summary>
///     Component interface for Composite pattern
///     Allows treating individual packages and groups uniformly
/// </summary>
public interface IPackageComponent
{
    /// <summary>
    ///     Gets the unique identifier
    /// </summary>
    string Id { get; }
    
    /// <summary>
    ///     Gets the description
    /// </summary>
    string Description { get; }
    
    /// <summary>
    ///     Gets the total weight (including children for composites)
    /// </summary>
    decimal GetTotalWeight();
    
    /// <summary>
    ///     Gets the package count (1 for leaf, sum for composite)
    /// </summary>
    int GetPackageCount();
    
    /// <summary>
    ///     Displays package information with indentation
    /// </summary>
    /// <param name="indent">Indentation level</param>
    void Display(int indent = 0);
    
    /// <summary>
    ///     Gets all individual packages (flattened)
    /// </summary>
    List<Entities.Package> GetAllPackages();
}
