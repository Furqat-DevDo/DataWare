namespace AviaSales.External.Services.Options;

/// <summary>
/// InMemeory cache options for http clients.
/// </summary>
public class CacheOptions
{
    /// <summary>
    /// SlidingExpiration time in minutes.
    /// </summary>
    public int SlidingTimeMinute { get; set; } = 10;
    
    /// <summary>
    /// Absolute Expiration Time in days
    /// </summary>
    public int AbsoluteTimeDay { get; set; } = 1;
}