namespace AviaSales.Shared.Entities;

/// <summary>
/// Interface representing an auditable entity with creation and update timestamps.
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// Gets or sets the timestamp indicating when the entity was created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the optional timestamp indicating when the entity was last updated.
    /// </summary>
    DateTime? UpdatedAt { get; set; }
}
