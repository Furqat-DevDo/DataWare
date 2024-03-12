using System.Text.Json.Serialization;
namespace AviaSales.External.Services.Models;

public class RestCountry
{
    [JsonPropertyName("name")]
    public Name Name { get; set; }

    [JsonPropertyName("cca2")]
    public string Cca2 { get; set; }

    [JsonPropertyName("ccn3")]
    public string Ccn3 { get; set; }

    [JsonPropertyName("cca3")]
    public string Cca3 { get; set; }

    [JsonPropertyName("cioc")]
    public string Cioc { get; set; }

    [JsonPropertyName("idd")]
    public Idd Idd { get; set; }

    [JsonPropertyName("capital")]
    public List<string> Capital { get; set; }

    [JsonPropertyName("area")]
    public double? Area { get; set; }
    
    [JsonPropertyName("timezones")]
    public List<string> Timezones { get; set; }
}

public class Idd
{
    [JsonPropertyName("root")]
    public string Root { get; set; }

    [JsonPropertyName("suffixes")]
    public List<string> Suffixes { get; set; }
}

public class Name
{
    [JsonPropertyName("common")]
    public string Common { get; set; }

    [JsonPropertyName("official")]
    public string Official { get; set; }
}
