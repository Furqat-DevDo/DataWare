using AviaSales.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents the price information associated with a particular entity.
/// </summary>
[Owned]
public class Price
{
    /// <summary>
    /// Gets or sets the amount of the price.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the type of the price, indicating the pricing category or classification.
    /// </summary>
    public EPriceType Type { get; set; }
}
