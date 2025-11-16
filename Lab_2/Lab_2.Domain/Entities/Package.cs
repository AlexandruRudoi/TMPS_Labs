using Lab_2.Domain.Enums;

namespace Lab_2.Domain.Entities;

/// <summary>
///     Package entity representing a deliverable item
/// </summary>
public class Package
{
    /// <summary>
    ///     Gets or sets the unique identifier for the package
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     Gets or sets the package description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Gets or sets the package weight in kilograms
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    ///     Gets or sets the delivery priority
    /// </summary>
    public PackagePriority Priority { get; set; }

    /// <summary>
    ///     Gets or sets whether the package requires refrigeration
    /// </summary>
    public bool RequiresRefrigeration { get; set; }

    /// <summary>
    ///     Gets or sets the destination address
    /// </summary>
    public string DestinationAddress { get; set; }

    /// <summary>
    ///     Gets or sets the destination region
    /// </summary>
    public string DestinationRegion { get; set; }

    /// <summary>
    ///     Initializes a new instance of the Package class
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="description">Package description</param>
    /// <param name="weight">Package weight in kg</param>
    /// <param name="destinationAddress">Destination address</param>
    /// <param name="destinationRegion">Destination region</param>
    public Package(string id, string description, decimal weight, string destinationAddress, string destinationRegion)
    {
        Id = id;
        Description = description;
        Weight = weight;
        DestinationAddress = destinationAddress;
        DestinationRegion = destinationRegion;
        Priority = PackagePriority.Standard;
        RequiresRefrigeration = false;
    }
}