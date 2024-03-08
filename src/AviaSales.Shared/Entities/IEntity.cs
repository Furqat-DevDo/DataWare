namespace AviaSales.Shared.Entities;

/// <summary>
/// Marker Interface of class
/// </summary>
public interface IEntity
{

}

/// <summary>
/// Entity with Id
/// </summary>
/// <typeparam name="TKey">Id type</typeparam>
public interface IEntity<TKey> : IEntity 
    where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
}