using System.Xml.Serialization;
using Serilog;

namespace AviaSales.Shared.Extensions;

public static  class StringExtensions
{
    /// <summary>
    /// Deserialize from to specified class
    /// </summary>
    /// <param name="xmlString">XML string.</param>
    /// <typeparam name="T">Response class type of T.</typeparam>
    public static async Task<T?> DeserializeXml<T>(this string xmlString)
    {
        try
        {
            using var stringReader = new StringReader(xmlString);
            var serializer = new XmlSerializer(typeof(T));

            return await Task.Run(() => (T)serializer.Deserialize(stringReader)!);
        }
        catch (Exception ex)
        {
            Log.Error(ex,"Error deserializing XML");
            return default;
        }
    }
}