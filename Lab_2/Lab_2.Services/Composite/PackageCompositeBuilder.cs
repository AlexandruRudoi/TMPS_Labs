using Lab_2.Domain.Entities;
using Lab_2.Domain.Interfaces;

namespace Lab_2.Services.Composite;

/// <summary>
///     Helper class for building package hierarchies using Composite pattern
/// </summary>
public class PackageCompositeBuilder
{
    /// <summary>
    ///     Creates a pallet with multiple boxes
    /// </summary>
    public static IPackageComponent CreatePallet(string palletId, List<Package> packages, int packagesPerBox = 3)
    {
        var pallet = new PackageContainer(palletId, "Shipping Pallet", "Pallet");
        
        // Group packages into boxes
        var boxNumber = 1;
        for (int i = 0; i < packages.Count; i += packagesPerBox)
        {
            var boxPackages = packages.Skip(i).Take(packagesPerBox).ToList();
            var box = CreateBox($"{palletId}-BOX-{boxNumber:D3}", boxPackages);
            pallet.Add(box);
            boxNumber++;
        }
        
        return pallet;
    }

    /// <summary>
    ///     Creates a box containing packages
    /// </summary>
    public static IPackageComponent CreateBox(string boxId, List<Package> packages)
    {
        var box = new PackageContainer(boxId, "Cardboard Box", "Box");
        
        foreach (var package in packages)
        {
            box.Add(new PackageLeaf(package));
        }
        
        return box;
    }

    /// <summary>
    ///     Creates a crate for heavy/fragile items
    /// </summary>
    public static IPackageComponent CreateCrate(string crateId, string description, List<Package> packages)
    {
        var crate = new PackageContainer(crateId, description, "Wooden Crate");
        
        foreach (var package in packages)
        {
            crate.Add(new PackageLeaf(package));
        }
        
        return crate;
    }

    /// <summary>
    ///     Creates a multi-level container hierarchy
    ///     Example: Pallet -> Crates -> Boxes -> Packages
    /// </summary>
    public static IPackageComponent CreateComplexHierarchy(string rootId, List<Package> packages)
    {
        // Create root pallet
        var rootPallet = new PackageContainer(rootId, "Master Shipping Pallet", "Master Pallet");
        
        // Divide packages into groups
        var midPoint = packages.Count / 2;
        
        // First crate with boxes
        var crate1 = new PackageContainer($"{rootId}-CRATE-001", "Electronics Crate", "Crate");
        var box1 = CreateBox($"{rootId}-CRATE-001-BOX-001", packages.Take(midPoint / 2).ToList());
        var box2 = CreateBox($"{rootId}-CRATE-001-BOX-002", packages.Skip(midPoint / 2).Take(midPoint / 2).ToList());
        crate1.Add(box1);
        crate1.Add(box2);
        
        // Second crate with boxes
        var crate2 = new PackageContainer($"{rootId}-CRATE-002", "General Cargo Crate", "Crate");
        var box3 = CreateBox($"{rootId}-CRATE-002-BOX-001", packages.Skip(midPoint).Take(packages.Count - midPoint).ToList());
        crate2.Add(box3);
        
        // Add crates to pallet
        rootPallet.Add(crate1);
        rootPallet.Add(crate2);
        
        return rootPallet;
    }

    /// <summary>
    ///     Creates a refrigerated container for temperature-sensitive items
    /// </summary>
    public static IPackageComponent CreateRefrigeratedContainer(string containerId, List<Package> packages)
    {
        var container = new PackageContainer(containerId, "Refrigerated Container (2°C - 8°C)", "Refrigerated Container");
        
        // Group by temperature requirements
        foreach (var package in packages.Where(p => p.RequiresRefrigeration))
        {
            container.Add(new PackageLeaf(package));
        }
        
        return container;
    }
}
