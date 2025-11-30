using Lab_3.Domain.Entities;
using Lab_3.Domain.Entities.Vehicles;
using Lab_3.Domain.Enums;
using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Builders;

/// <summary>
///     Builder pattern implementation for step-by-step shipment construction
/// </summary>
public class ShipmentBuilder : IShipmentBuilder
{
    /// <summary>
    ///     Shipment instance being constructed
    /// </summary>
    private Shipment _shipment;

    /// <summary>
    ///     Initializes a new instance of the ShipmentBuilder
    /// </summary>
    public ShipmentBuilder()
    {
        Reset();
    }

    /// <inheritdoc />
    public void Reset()
    {
        _shipment = new Shipment();
    }

    /// <inheritdoc />
    public IShipmentBuilder SetId(string id)
    {
        _shipment.Id = id;
        return this;
    }

    /// <inheritdoc />
    public IShipmentBuilder AddPackage(Package package)
    {
        if (package == null)
            throw new ArgumentNullException(nameof(package));

        _shipment.Packages.Add(package);
        return this;
    }

    /// <inheritdoc />
    public IShipmentBuilder AddPackages(IEnumerable<Package> packages)
    {
        if (packages == null)
            throw new ArgumentNullException(nameof(packages));

        _shipment.Packages.AddRange(packages);
        return this;
    }

    /// <inheritdoc />
    public IShipmentBuilder AssignVehicle(Vehicle vehicle)
    {
        _shipment.AssignedVehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle));
        return this;
    }

    /// <inheritdoc />
    public IShipmentBuilder AssignDriver(Driver driver)
    {
        _shipment.AssignedDriver = driver ?? throw new ArgumentNullException(nameof(driver));
        return this;
    }

    /// <inheritdoc />
    public IShipmentBuilder SetRoute(Route route)
    {
        _shipment.Route = route ?? throw new ArgumentNullException(nameof(route));
        return this;
    }

    /// <inheritdoc />
    public IShipmentBuilder SetScheduledDate(DateTime date)
    {
        if (date < DateTime.Now)
            throw new ArgumentException("Scheduled date cannot be in the past");

        _shipment.ScheduledDate = date;
        return this;
    }

    /// <inheritdoc />
    public IShipmentBuilder AddNotes(string notes)
    {
        _shipment.Notes = notes;
        return this;
    }

    /// <inheritdoc />
    public Shipment Build()
    {
        if (string.IsNullOrEmpty(_shipment.Id))
            throw new InvalidOperationException("Shipment ID is required");

        if (!_shipment.Packages.Any())
            throw new InvalidOperationException("Shipment must have at least one package");

        if (_shipment.AssignedVehicle == null)
            throw new InvalidOperationException("Vehicle must be assigned");

        if (_shipment.AssignedDriver == null)
            throw new InvalidOperationException("Driver must be assigned");

        if (_shipment.Route == null)
            throw new InvalidOperationException("Route must be assigned");

        _shipment.CalculateTotalWeight();

        if (_shipment.TotalWeight > _shipment.AssignedVehicle.Capacity)
            throw new InvalidOperationException(
                $"Total weight ({_shipment.TotalWeight}kg) exceeds vehicle capacity ({_shipment.AssignedVehicle.Capacity}kg)");

        if (_shipment.ScheduledDate != default)
            _shipment.Status = ShipmentStatus.Scheduled;

        var result = _shipment;
        Reset();
        return result;
    }
}