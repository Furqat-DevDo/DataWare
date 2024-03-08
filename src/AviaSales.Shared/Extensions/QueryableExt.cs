using AviaSales.Shared.Models;

namespace AviaSales.Shared.Extensions;

/// <summary>
/// Extension methods for IQueryable, providing a simple way to implement paging.
/// </summary>
public static class QueryableExt
{
    /// <summary>
    /// Applies paging to an IQueryable collection based on the specified Pager configuration.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entities in the collection.</typeparam>
    /// <param name="query">The IQueryable collection to be paged.</param>
    /// <param name="pager">Pager configuration specifying the current page and items per page.</param>
    /// <returns>An IQueryable collection representing the specified page of entities.</returns>
    public static IQueryable<TEntity> Paged<TEntity>(this IQueryable<TEntity> query, Pager pager)
    {
        query = query
            .Skip((pager.Page - 1) * pager.PerPage)
            .Take(pager.PerPage);
        
        return query;
    }
}


