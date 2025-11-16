namespace Lab_2.Domain.Interfaces;

/// <summary>
///     Object Pool pattern interface for managing reusable resources
/// </summary>
/// <typeparam name="T">Type of resource to pool</typeparam>
public interface IResourcePool<T> where T : class
{
    /// <summary>
    ///     Acquires a resource from the pool
    /// </summary>
    /// <returns>Available resource instance or null if none available</returns>
    T Acquire();

    /// <summary>
    ///     Returns a resource back to the pool
    /// </summary>
    /// <param name="resource">Resource to release</param>
    void Release(T resource);

    /// <summary>
    ///     Gets the number of currently available resources
    /// </summary>
    int AvailableCount { get; }

    /// <summary>
    ///     Gets the total number of resources in the pool
    /// </summary>
    int TotalCount { get; }
}