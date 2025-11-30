using Lab_3.Domain.Entities;

namespace Lab_3.Domain.Interfaces;

/// <summary>
///     Prototype pattern interface for cloning routes
/// </summary>
public interface IRoutePrototype
{
    /// <summary>
    ///     Creates a deep clone of the route
    /// </summary>
    /// <returns>Cloned route instance</returns>
    Route Clone();

    /// <summary>
    ///     Creates a clone with modified identifier and name
    /// </summary>
    /// <param name="newId">New route identifier</param>
    /// <param name="newName">New route name</param>
    /// <returns>Modified cloned route instance</returns>
    Route CloneWithModifications(string newId, string newName);
}