using Lab_1.Domain.Enums;

namespace Lab_1.Domain.Entities;

/// <summary>
///     Route entity that can be cloned using Prototype pattern
/// </summary>
public class Route
{
    /// <summary>
    ///     Gets or sets the unique identifier for the route
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     Gets or sets the route name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the operational region
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    ///     Gets or sets the list of waypoints along the route
    /// </summary>
    public List<string> Waypoints { get; set; }

    /// <summary>
    ///     Gets or sets the total distance in kilometers
    /// </summary>
    public decimal TotalDistance { get; set; }

    /// <summary>
    ///     Gets or sets the estimated travel duration
    /// </summary>
    public TimeSpan EstimatedDuration { get; set; }

    /// <summary>
    ///     Gets or sets the route type
    /// </summary>
    public RouteType Type { get; set; }

    /// <summary>
    ///     Initializes a new instance of the Route class
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="name">Route name</param>
    /// <param name="region">Operational region</param>
    public Route(string id, string name, string region)
    {
        Id = id;
        Name = name;
        Region = region;
        Waypoints = new List<string>();
        Type = RouteType.Standard;
    }

    /// <summary>
    ///     Creates a deep clone of the route
    /// </summary>
    /// <returns>Cloned route instance</returns>
    public Route Clone()
    {
        var clone = new Route(Id, Name, Region)
        {
            TotalDistance = this.TotalDistance,
            EstimatedDuration = this.EstimatedDuration,
            Type = this.Type,
            Waypoints = new List<string>(this.Waypoints)
        };
        return clone;
    }

    /// <summary>
    ///     Gets formatted information about the route
    /// </summary>
    /// <returns>String representation of route details</returns>
    public string GetRouteInfo()
    {
        return
            $"Route {Name} ({Region}): {TotalDistance}km, {Waypoints.Count} stops, Est. {EstimatedDuration.Hours}h {EstimatedDuration.Minutes}m";
    }
}