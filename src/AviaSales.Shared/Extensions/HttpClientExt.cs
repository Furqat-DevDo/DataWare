using System.Text.Json;
using Serilog;

namespace AviaSales.Shared.Extensions;

public static class HttpClientExt
{
    /// <summary>
    /// Sends a GET request to the specified URI and deserializes the JSON content from the response stream asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of object to deserialize.</typeparam>
    /// <param name="httpClient">The HttpClient instance.</param>
    /// <param name="requestUri">The URI of the resource to request.</param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains the deserialized object of type <typeparamref name="T"/>
    /// or the default value if deserialization fails.
    /// </returns>
    public static async Task<T?> GetJsonAsync<T>(this HttpClient httpClient, string requestUri)
    {
        using var response = await httpClient.GetAsync(requestUri);
        //response.EnsureSuccessStatusCode();

        await using var responseStream = await response.Content.ReadAsStreamAsync();
        return await DeserializeJsonAsync<T>(responseStream);
    }

    // Include your existing DeserializeJsonAsync method here

    private static async Task<T?> DeserializeJsonAsync<T>(Stream responseStream)
    {
        try
        {
            return await JsonSerializer.DeserializeAsync<T>(responseStream) ?? default!;
        }
        catch (JsonException ex)
        {
            Log.Error($"Error deserializing JSON content: {ex.Message}");
            return default!;
        }
    }
}