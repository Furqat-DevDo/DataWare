namespace AviaSales.Shared.Entities;

/// <summary>
/// The `IDeletable` interface declares a single property `IsDeleted` of type `bool`.
/// Allowing classes that implement this interface to indicate whether they are deleted or not.
/// </summary>
public interface IDeletable
{
    public bool IsDeleted { get; set; }
}