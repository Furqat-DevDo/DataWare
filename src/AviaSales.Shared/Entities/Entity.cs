namespace AviaSales.Shared.Entities;

/// <summary>
/// Generic class representing an entity with an identifier and deletion status.
/// </summary>
/// <typeparam name="T">Type of the entity identifier implementing IEquatable{T}.</typeparam>
public abstract class Entity<T> : IEntity<T>, IDeletable
    where T : IEquatable<T>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public T Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is marked as deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
}

/// <summary>
/// Non-generic class representing an entity with a Guid identifier, inheriting from the generic Entity{T} class.
/// </summary>
public abstract class Entity : Entity<Guid>
{

}