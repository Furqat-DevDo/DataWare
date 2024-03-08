using System.Linq.Expressions;
using AviaSales.Shared.Entities;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AviaSales.Shared.Managers;

/// <summary>
/// Base abstract class for implementing generic entity managers in an Entity Framework Core context.
/// </summary>
/// <typeparam name="TContext">The type of the Entity Framework context.</typeparam>
/// <typeparam name="TEntity">The type of the entity being managed.</typeparam>
/// <typeparam name="TModel">The type of the model associated with the entity.</typeparam>
public abstract class BaseManager<TContext, TEntity, TModel>
    where TEntity : class, IEntity
    where TContext : DbContext
{
    /// <summary>
    /// The Entity Framework context instance.
    /// </summary>
    protected TContext _db;

    /// <summary>
    /// Initializes a new instance of the BaseManager class.
    /// </summary>
    /// <param name="db">The Entity Framework context.</param>
    protected BaseManager(TContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Gets or sets the expression to convert the entity to its associated model.
    /// </summary>
    protected abstract Expression<Func<TEntity, TModel>> EntityToModel { get; }

    /// <summary>
    /// Retrieves a single model based on the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entities.</param>
    /// <returns>A single model or null if not found.</returns>
    public async ValueTask<TModel?> Get(Expression<Func<TEntity, bool>>? predicate)
    {
        var query = Set().AsNoTracking();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.Select(EntityToModel).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Retrieves a list of models based on the specified pager and predicate.
    /// </summary>
    /// <param name="pager">Pager configuration for pagination.</param>
    /// <param name="predicate">The predicate to filter the entities.</param>
    /// <returns>A list of models.</returns>
    public async ValueTask<List<TModel>> GetList(Pager pager, Expression<Func<TEntity, bool>>? predicate)
    {
        var query = Set().AsNoTracking();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        query = query.Paged(pager);

        return await query.Select(EntityToModel).ToListAsync();
    }

    /// <summary>
    /// Retrieves a list of models based on the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entities.</param>
    /// <returns>A list of models.</returns>
    public async ValueTask<List<TModel>> GetList(Expression<Func<TEntity, bool>> predicate)
    {
        var query = Set().AsNoTracking().Where(predicate);
        return await query.Select(EntityToModel).ToListAsync();
    }

    /// <summary>
    /// Retrieves a list of all models.
    /// </summary>
    /// <returns>A list of all models.</returns>
    public async ValueTask<List<TModel>> GetList()
    {
        var query = Set().AsNoTracking();
        return await query.Select(EntityToModel).ToListAsync();
    }

    /// <summary>
    /// Gets the DbSet for the specified entity type.
    /// </summary>
    /// <returns>The DbSet for the entity type.</returns>
    protected DbSet<TEntity> Set()
    {
        return _db.Set<TEntity>();
    }
}
