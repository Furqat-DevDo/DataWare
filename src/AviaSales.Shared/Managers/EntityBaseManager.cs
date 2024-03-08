using System.Linq.Expressions;
using AviaSales.Shared.Entities;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AviaSales.Shared.Managers;

/// <summary>
/// Abstract class representing a manager for entities with a unique identifier in an Entity Framework Core context.
/// </summary>
/// <typeparam name="TContext">The type of the Entity Framework context.</typeparam>
/// <typeparam name="TEntity">The type of the entity being managed.</typeparam>
/// <typeparam name="TKey">The type of the unique identifier for the entity.</typeparam>
/// <typeparam name="TModel">The type of the model associated with the entity.</typeparam>
public abstract class EntityBaseManager<TContext, TEntity, TKey, TModel> 
    : BaseManager<TContext, TEntity, TModel>, IManager
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the EntityBaseManager class.
    /// </summary>
    /// <param name="db">The Entity Framework context.</param>
    protected EntityBaseManager(TContext db) : base(db)
    {
    }

    /// <summary>
    /// Retrieves a paginated list of models based on the specified pager and predicate.
    /// </summary>
    /// <param name="pager">Pager configuration for pagination.</param>
    /// <param name="predicate">The predicate to filter the entities.</param>
    /// <returns>A paginated list of models.</returns>
    public virtual async ValueTask<List<TModel>> GetListAsync(Pager pager, Expression<Func<TEntity, bool>>? predicate)
    {
        IQueryable<TEntity> query = Set().AsNoTracking();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query
            .Paged(pager)
            .Select(EntityToModel)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a model by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>A model or null if not found.</returns>
    public virtual async ValueTask<TModel?> GetByIdAsync(TKey id)
    {
        return await Set()
            .AsNoTracking()
            .Where(entity => entity.Id.Equals(id))
            .Select(EntityToModel)
            .FirstOrDefaultAsync();
    }
}
