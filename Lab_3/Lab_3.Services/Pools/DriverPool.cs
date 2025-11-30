using Lab_3.Domain.Entities;
using Lab_3.Domain.Enums;
using Lab_3.Domain.Interfaces;

namespace Lab_3.Services.Pools;

/// <summary>
///     Object Pool pattern for driver resource management
/// </summary>
public class DriverPool : IResourcePool<Driver>
{
    /// <summary>
    ///     List of available drivers in the pool
    /// </summary>
    private readonly List<Driver> _available;

    /// <summary>
    ///     List of drivers currently in use
    /// </summary>
    private readonly List<Driver> _inUse;

    /// <summary>
    ///     Lock object for thread-safe operations
    /// </summary>
    private readonly object _lock = new();

    /// <summary>
    ///     Initializes a new instance with the specified drivers
    /// </summary>
    /// <param name="drivers">Initial collection of drivers</param>
    public DriverPool(IEnumerable<Driver> drivers)
    {
        _available = new List<Driver>(drivers);
        _inUse = new List<Driver>();
    }

    /// <inheritdoc />
    public int AvailableCount => _available.Count;

    /// <inheritdoc />
    public int TotalCount => _available.Count + _inUse.Count;

    /// <inheritdoc />
    public Driver Acquire()
    {
        lock (_lock)
        {
            if (_available.Count == 0) throw new InvalidOperationException("No drivers available in the pool");

            var driver = _available[0];
            _available.RemoveAt(0);
            _inUse.Add(driver);

            driver.Status = DriverStatus.OnRoute;
            return driver;
        }
    }

    /// <inheritdoc />
    public void Release(Driver driver)
    {
        lock (_lock)
        {
            if (!_inUse.Contains(driver)) throw new InvalidOperationException("Driver is not currently in use");

            _inUse.Remove(driver);
            _available.Add(driver);

            driver.Status = DriverStatus.Available;
        }
    }

    /// <summary>
    ///     Acquires a driver with minimum license type from the pool
    /// </summary>
    /// <param name="minLicenseType">Minimum required license type</param>
    /// <returns>Driver with appropriate license</returns>
    /// <exception cref="InvalidOperationException">Thrown when no driver with required license is available</exception>
    public Driver AcquireWithLicense(DriverLicenseType minLicenseType)
    {
        lock (_lock)
        {
            var driver = _available.FirstOrDefault(d => d.LicenseType >= minLicenseType);

            if (driver == null)
                throw new InvalidOperationException(
                    $"No drivers with license type {minLicenseType} or higher available");

            _available.Remove(driver);
            _inUse.Add(driver);

            driver.Status = DriverStatus.OnRoute;
            return driver;
        }
    }

    /// <summary>
    ///     Gets a copy of all available drivers
    /// </summary>
    /// <returns>List of available drivers</returns>
    public List<Driver> GetAvailableDrivers()
    {
        lock (_lock)
        {
            return new List<Driver>(_available);
        }
    }

    /// <summary>
    ///     Gets a copy of all drivers currently in use
    /// </summary>
    /// <returns>List of in-use drivers</returns>
    public List<Driver> GetInUseDrivers()
    {
        lock (_lock)
        {
            return new List<Driver>(_inUse);
        }
    }
}