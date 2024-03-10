using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;

namespace AviaSales.Shared.Extensions;

/// <summary>
/// Provides extension methods for working with distributed caching through the <see cref="IDistributedCache"/> interface.
/// </summary>
public static class DistributedCacheExt
{
    /// <summary>
    /// Asynchronously sets a record in the cache with the specified identifier and data.
    /// </summary>
    /// <typeparam name="T">The type of data to be stored in the cache.</typeparam>
    /// <param name="cache">The distributed cache instance.</param>
    /// <param name="recordId">Identifier for the cache record.</param>
    /// <param name="data">Data to be stored in the cache.</param>
    /// <param name="options">Optional additional options for cache entry, such as expiration and sliding expiration.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data, 
        DistributedCacheEntryOptions options = default)
    {
        var jsonData = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(recordId, jsonData, options);
    }

    /// <summary>
    /// Asynchronously retrieves a cached record based on the provided identifier.
    /// </summary>
    /// <typeparam name="T">The type of data to be retrieved from the cache.</typeparam>
    /// <param name="cache">The distributed cache instance.</param>
    /// <param name="recordId">Identifier for the cache record.</param>
    /// <returns>A <see cref="Task"/> containing the deserialized data of type <typeparamref name="T"/> or the default value if the record is not found.</returns>
    public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
    {
        var jsonData = await cache.GetStringAsync(recordId);
        if (jsonData is null) return default(T);

        return JsonSerializer.Deserialize<T>(jsonData);
    }

    /// <summary>
    /// Asynchronously sets a value in the cache with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of data to be stored in the cache.</typeparam>
    /// <param name="cache">The distributed cache instance.</param>
    /// <param name="key">Key for the cache entry.</param>
    /// <param name="value">Value to be stored in the cache.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static Task SetAsync<T>(this IDistributedCache cache, string key, T value)
    {
        return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
    }

    /// <summary>
    /// Asynchronously sets a value in the cache with the specified key and additional options.
    /// </summary>
    /// <typeparam name="T">The type of data to be stored in the cache.</typeparam>
    /// <param name="cache">The distributed cache instance.</param>
    /// <param name="key">Key for the cache entry.</param>
    /// <param name="value">Value to be stored in the cache.</param>
    /// <param name="options">Additional options for cache entry.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
    {
        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, GetJsonSerializerOptions()));
        return cache.SetAsync(key, bytes, options);
    }

    /// <summary>
    /// Attempts to retrieve a value from the cache with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of data to be retrieved from the cache.</typeparam>
    /// <param name="cache">The distributed cache instance.</param>
    /// <param name="key">Key for the cache entry.</param>
    /// <param name="value">The retrieved value or default if the key is not found.</param>
    /// <returns><c>true</c> if the retrieval was successful; otherwise, <c>false</c>.</returns>
    public static bool TryGetValue<T>(this IDistributedCache cache, string key, out T? value)
    {
        var val = cache.Get(key);
        value = default;
        if (val == null) return false;
        value = JsonSerializer.Deserialize<T>(val, GetJsonSerializerOptions());
        return true;
    }

    /// <summary>
    /// Gets custom <see cref="JsonSerializerOptions"/> for serializing and deserializing JSON.
    /// </summary>
    /// <returns>An instance of <see cref="JsonSerializerOptions"/> configured with specific settings.</returns>
    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,
            WriteIndented = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
}
