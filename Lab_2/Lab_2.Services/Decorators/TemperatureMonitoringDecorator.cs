using Lab_2.Domain.Interfaces;

namespace Lab_2.Services.Decorators;

/// <summary>
///     Concrete Decorator - Adds temperature monitoring to shipment
/// </summary>
public class TemperatureMonitoringDecorator : ShipmentDecorator
{
    private readonly decimal _minTemperature;
    private readonly decimal _maxTemperature;
    private readonly decimal _monitoringFee;

    /// <summary>
    ///     Initializes a new instance of the TemperatureMonitoringDecorator
    /// </summary>
    /// <param name="shipment">The shipment to monitor</param>
    /// <param name="minTemp">Minimum allowed temperature (°C)</param>
    /// <param name="maxTemp">Maximum allowed temperature (°C)</param>
    public TemperatureMonitoringDecorator(IShipmentComponent shipment, decimal minTemp = 2m, decimal maxTemp = 8m) 
        : base(shipment)
    {
        _minTemperature = minTemp;
        _maxTemperature = maxTemp;
        _monitoringFee = 15.0m;
    }

    /// <inheritdoc />
    public override string GetDescription()
    {
        return $"{base.GetDescription()} + Temperature Monitoring ({_minTemperature}°C - {_maxTemperature}°C)";
    }

    /// <inheritdoc />
    public override decimal CalculateCost()
    {
        return base.CalculateCost() + _monitoringFee;
    }

    /// <inheritdoc />
    public override void Process()
    {
        base.Process();
        Console.WriteLine($"  Temperature monitoring: {_minTemperature}°C - {_maxTemperature}°C (Fee: ${_monitoringFee:F2})");
        Console.WriteLine($"    Current temperature: {GetCurrentTemperature()}°C - Status: OK");
    }

    /// <summary>
    ///     Simulates current temperature reading
    /// </summary>
    private decimal GetCurrentTemperature()
    {
        var random = new Random();
        return _minTemperature + (decimal)(random.NextDouble() * (double)(_maxTemperature - _minTemperature));
    }
}
