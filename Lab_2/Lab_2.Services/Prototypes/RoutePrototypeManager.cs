using Lab_2.Domain.Entities;
using Lab_2.Domain.Enums;
using Lab_2.Domain.Interfaces;

namespace Lab_2.Services.Prototypes;

/// <summary>
///     Prototype pattern implementation for route template management
/// </summary>
public class RoutePrototypeManager : IRoutePrototype
{
    /// <summary>
    ///     Dictionary storing route templates by key
    /// </summary>
    private readonly Dictionary<string, Route> _routeTemplates;

    /// <summary>
    ///     Initializes a new instance with default route templates
    /// </summary>
    public RoutePrototypeManager()
    {
        _routeTemplates = new Dictionary<string, Route>();
        InitializeDefaultTemplates();
    }

    /// <summary>
    ///     Creates default route templates for urban, rural, and express routes
    /// </summary>
    private void InitializeDefaultTemplates()
    {
        // Urban route template
        var urbanRoute = new Route("URBAN-001", "City Center Loop", "Urban")
        {
            TotalDistance = 25m,
            EstimatedDuration = TimeSpan.FromHours(2),
            Type = RouteType.Urban,
            Waypoints = new List<string>
            {
                "Main Street",
                "Central Plaza",
                "Business District",
                "Shopping Mall",
                "Residential Area"
            }
        };
        _routeTemplates.Add("urban-template", urbanRoute);

        // Rural route template
        var ruralRoute = new Route("RURAL-001", "Countryside Circuit", "Rural")
        {
            TotalDistance = 80m,
            EstimatedDuration = TimeSpan.FromHours(4),
            Type = RouteType.Rural,
            Waypoints = new List<string>
            {
                "Village North",
                "Farmland Area",
                "Mountain Pass",
                "Lake District",
                "Village South"
            }
        };
        _routeTemplates.Add("rural-template", ruralRoute);

        // Express route template
        var expressRoute = new Route("EXPRESS-001", "Priority Delivery", "Urban")
        {
            TotalDistance = 15m,
            EstimatedDuration = TimeSpan.FromMinutes(45),
            Type = RouteType.Express,
            Waypoints = new List<string>
            {
                "Distribution Center",
                "Express Hub",
                "Priority Zone"
            }
        };
        _routeTemplates.Add("express-template", expressRoute);
    }

    /// <summary>
    ///     Registers a new route template with the specified key
    /// </summary>
    /// <param name="key">Unique identifier for the template</param>
    /// <param name="route">Route instance to use as template</param>
    public void RegisterTemplate(string key, Route route)
    {
        _routeTemplates[key] = route;
    }

    /// <summary>
    ///     Retrieves a route template by key
    /// </summary>
    /// <param name="key">Template identifier</param>
    /// <returns>Route template or null if not found</returns>
    public Route GetTemplate(string key)
    {
        return _routeTemplates.ContainsKey(key) ? _routeTemplates[key] : null;
    }

    /// <inheritdoc />
    public Route Clone()
    {
        var firstTemplate = _routeTemplates.Values.FirstOrDefault();
        return firstTemplate?.Clone();
    }

    /// <inheritdoc />
    public Route CloneTemplate(string templateKey)
    {
        if (!_routeTemplates.ContainsKey(templateKey))
            throw new ArgumentException($"Template '{templateKey}' not found");

        return _routeTemplates[templateKey].Clone();
    }

    /// <inheritdoc />
    public Route CloneWithModifications(string newId, string newName)
    {
        var template = _routeTemplates.Values.FirstOrDefault();
        if (template == null)
            throw new InvalidOperationException("No templates available");

        var clone = template.Clone();
        clone.Id = newId;
        clone.Name = newName;
        return clone;
    }

    /// <inheritdoc />
    public Route CloneTemplateWithModifications(string templateKey, string newId, string newName)
    {
        if (!_routeTemplates.ContainsKey(templateKey))
            throw new ArgumentException($"Template '{templateKey}' not found");

        var clone = _routeTemplates[templateKey].Clone();
        clone.Id = newId;
        clone.Name = newName;
        return clone;
    }

    /// <summary>
    ///     Gets list of all available template keys
    /// </summary>
    /// <returns>List of template identifiers</returns>
    public List<string> GetAvailableTemplates()
    {
        return _routeTemplates.Keys.ToList();
    }
}