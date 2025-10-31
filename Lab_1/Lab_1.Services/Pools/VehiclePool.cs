using Lab_1.Domain.Entities.Vehicles;
using Lab_1.Domain.Enums;
using Lab_1.Domain.Interfaces;

namespace Lab_1.Services.Pools;

/// <summary>
///     Object Pool pattern for vehicle resource management
/// </summary>
public class VehiclePool : IResourcePool<Vehicle>
{
    /// <summary>
    ///     List of available vehicles in the pool
    /// </summary>
    private readonly List<Vehicle> _available;
    
    /// <summary>
    ///     List of vehicles currently in use
    /// </summary>
    private readonly List<Vehicle> _inUse;
    
    /// <summary>
    ///     Lock object for thread-safe operations
    /// </summary>
    private readonly object _lock = new object();

    /// <inheritdoc />
    public int AvailableCount => _available.Count;
    
    /// <inheritdoc />
    public int TotalCount => _available.Count + _inUse.Count;

    /// <summary>
    ///     Initializes a new instance with the specified vehicles
    /// </summary>
    /// <param name="vehicles">Initial collection of vehicles</param>
    public VehiclePool(IEnumerable<Vehicle> vehicles)
    {
        _available = new List<Vehicle>(vehicles);
        _inUse = new List<Vehicle>();
    }

    /// <inheritdoc />
    public Vehicle Acquire()
    {
        lock (_lock)
        {
            if (_available.Count == 0)
            {
                throw new InvalidOperationException("No vehicles available in the pool");
            }

            var vehicle = _available[0];
            _available.RemoveAt(0);
            _inUse.Add(vehicle);
            
            vehicle.Status = VehicleStatus.InUse;
            return vehicle;
        }
    }

    /// <summary>
    ///     Acquires a vehicle of the specified type from the pool
    /// </summary>
    /// <param name="type">Type of vehicle to acquire</param>
    /// <returns>Vehicle of the requested type</returns>
    /// <exception cref="InvalidOperationException">Thrown when no vehicle of specified type is available</exception>
    public Vehicle AcquireByType(VehicleType type)
    {
        lock (_lock)
        {
            var vehicle = _available.FirstOrDefault(v => v.Type == type);
            
            if (vehicle == null)
            {
                throw new InvalidOperationException($"No {type} vehicles available in the pool");
            }

            _available.Remove(vehicle);
            _inUse.Add(vehicle);
            
            vehicle.Status = VehicleStatus.InUse;
            return vehicle;
        }
    }

    /// <inheritdoc />
    public void Release(Vehicle vehicle)
    {
        lock (_lock)
        {
            if (!_inUse.Contains(vehicle))
            {
                throw new InvalidOperationException("Vehicle is not currently in use");
            }

            _inUse.Remove(vehicle);
            _available.Add(vehicle);
            
            vehicle.Status = VehicleStatus.Available;
        }
    }

    /// <summary>
    ///     Gets a copy of all available vehicles
    /// </summary>
    /// <returns>List of available vehicles</returns>
    public List<Vehicle> GetAvailableVehicles()
    {
        lock (_lock)
        {
            return new List<Vehicle>(_available);
        }
    }

    /// <summary>
    ///     Gets a copy of all vehicles currently in use
    /// </summary>
    /// <returns>List of in-use vehicles</returns>
    public List<Vehicle> GetInUseVehicles()
    {
        lock (_lock)
        {
            return new List<Vehicle>(_inUse);
        }
    }
}
