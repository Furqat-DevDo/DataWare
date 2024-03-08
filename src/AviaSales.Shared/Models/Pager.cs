﻿namespace AviaSales.Shared.Models;

/// <summary>
/// Represents a paging configuration with default values for page number and items per page.
/// </summary>
public record Pager
{
    /// <summary>
    /// Gets or sets the current page number. Defaults to 1.
    /// </summary>
    public ushort Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items to display per page. Defaults to 10.
    /// </summary>
    public byte PerPage { get; set; } = 10;
}