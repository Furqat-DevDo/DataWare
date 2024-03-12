using System.Text.Json.Serialization;
namespace AviaSales.External.Services.Models;

public class RestCountry
{
    [JsonPropertyName("name")]
    public Name Name { get; set; }

    [JsonPropertyName("tld")]
    public List<string> Tld { get; set; }

    [JsonPropertyName("cca2")]
    public string Cca2 { get; set; }

    [JsonPropertyName("ccn3")]
    public string Ccn3 { get; set; }

    [JsonPropertyName("cca3")]
    public string Cca3 { get; set; }

    [JsonPropertyName("cioc")]
    public string Cioc { get; set; }

    [JsonPropertyName("independent")]
    public bool? Independent { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("unMember")]
    public bool? UnMember { get; set; }

    [JsonPropertyName("currencies")]
    public Currencies Currencies { get; set; }

    [JsonPropertyName("idd")]
    public Idd Idd { get; set; }

    [JsonPropertyName("capital")]
    public List<string> Capital { get; set; }

    [JsonPropertyName("altSpellings")]
    public List<string> AltSpellings { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("subregion")]
    public string Subregion { get; set; }

    [JsonPropertyName("languages")]
    public Languages Languages { get; set; }

    [JsonPropertyName("latlng")]
    public List<double?> Latlng { get; set; }

    [JsonPropertyName("landlocked")]
    public bool? Landlocked { get; set; }

    [JsonPropertyName("area")]
    public double? Area { get; set; }

    [JsonPropertyName("demonyms")]
    public Demonyms Demonyms { get; set; }

    [JsonPropertyName("flag")]
    public string Flag { get; set; }

    [JsonPropertyName("maps")]
    public Maps Maps { get; set; }

    [JsonPropertyName("population")]
    public int? Population { get; set; }

    [JsonPropertyName("gini")]
    public Gini Gini { get; set; }

    [JsonPropertyName("fifa")]
    public string Fifa { get; set; }

    [JsonPropertyName("car")]
    public Car Car { get; set; }

    [JsonPropertyName("timezones")]
    public List<string> Timezones { get; set; }

    [JsonPropertyName("continents")]
    public List<string> Continents { get; set; }

    [JsonPropertyName("flags")]
    public Flags Flags { get; set; }

    [JsonPropertyName("coatOfArms")]
    public CoatOfArms CoatOfArms { get; set; }

    [JsonPropertyName("startOfWeek")]
    public string StartOfWeek { get; set; }

    [JsonPropertyName("capitalInfo")]
    public CapitalInfo CapitalInfo { get; set; }
    
}

public class CapitalInfo
{
    [JsonPropertyName("latlng")]
    public List<double?> Latlng { get; set; }
}

public class Car
{
    [JsonPropertyName("signs")]
    public List<string> Signs { get; set; }

    [JsonPropertyName("side")]
    public string Side { get; set; }
}

public class CoatOfArms
{
    [JsonPropertyName("png")]
    public string Png { get; set; }

    [JsonPropertyName("svg")]
    public string Svg { get; set; }
}

public class Currencies
{
    [JsonPropertyName("EUR")]
    public EUR EUR { get; set; }
}

public class Cym
{
    [JsonPropertyName("official")]
    public string Official { get; set; }

    [JsonPropertyName("common")]
    public string Common { get; set; }
}

public class Demonyms
{
    [JsonPropertyName("eng")]
    public Eng Eng { get; set; }

    [JsonPropertyName("fra")]
    public Fra Fra { get; set; }
}

public class Eng
{
    [JsonPropertyName("f")]
    public string F { get; set; }

    [JsonPropertyName("m")]
    public string M { get; set; }
}

public class EUR
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
}

public class Flags
{
    [JsonPropertyName("png")]
    public string Png { get; set; }

    [JsonPropertyName("svg")]
    public string Svg { get; set; }

    [JsonPropertyName("alt")]
    public string Alt { get; set; }
}

public class Fra
{
    [JsonPropertyName("official")]
    public string Official { get; set; }

    [JsonPropertyName("common")]
    public string Common { get; set; }

    [JsonPropertyName("f")]
    public string F { get; set; }

    [JsonPropertyName("m")]
    public string M { get; set; }
}

public class Gini
{
    [JsonPropertyName("2018")]
    public double? _2018 { get; set; }
}

public class Idd
{
    [JsonPropertyName("root")]
    public string Root { get; set; }

    [JsonPropertyName("suffixes")]
    public List<string> Suffixes { get; set; }
}

public class Languages
{
    [JsonPropertyName("ell")]
    public string Ell { get; set; }

    [JsonPropertyName("tur")]
    public string Tur { get; set; }
}

public class Maps
{
    [JsonPropertyName("googleMaps")]
    public string GoogleMaps { get; set; }

    [JsonPropertyName("openStreetMaps")]
    public string OpenStreetMaps { get; set; }
}

public class Name
{
    [JsonPropertyName("common")]
    public string Common { get; set; }

    [JsonPropertyName("official")]
    public string Official { get; set; }
}
