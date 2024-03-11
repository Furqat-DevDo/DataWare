using System.Text.Json.Serialization;

namespace AviaSales.External.Services.Models;

    public class _0
    {
        [JsonPropertyName("airport")]
        public string Airport { get; set; }

        [JsonPropertyName("stopDuration")]
        public int StopDuration { get; set; }
    }

    public class _1
    {
        [JsonPropertyName("airport")]
        public string Airport { get; set; }

        [JsonPropertyName("stopDuration")]
        public int StopDuration { get; set; }
    }

    public class ArrivalAirport
    {
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("tz")]
        public string Tz { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("country")]
        public Country Country { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }

    public class Baggage
    {
        [JsonPropertyName("cabin")]
        public Cabin Cabin { get; set; }

        [JsonPropertyName("checkIn")]
        public CheckIn CheckIn { get; set; }
    }

    public class Cabin
    {
        [JsonPropertyName("qty")]
        public int Qty { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("allowance")]
        public int Allowance { get; set; }
    }

    public class CheckIn
    {
        [JsonPropertyName("allowance")]
        public int Allowance { get; set; }

        [JsonPropertyName("qty")]
        public string Qty { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("refNumber")]
        public int RefNumber { get; set; }
    }

    public class Country2
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
    }

    public class DepartureAirport
    {
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("tz")]
        public string Tz { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("country")]
        public Country Country { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }

    public class Duration
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("careerCode")]
        public string CareerCode { get; set; }

        [JsonPropertyName("flight_code")]
        public string FlightCode { get; set; }

        [JsonPropertyName("flight_name")]
        public string FlightName { get; set; }

        [JsonPropertyName("stops")]
        public string Stops { get; set; }

        [JsonPropertyName("cabinType")]
        public string CabinType { get; set; }

        [JsonPropertyName("baggage")]
        public Baggage Baggage { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("departureAirport")]
        public DepartureAirport DepartureAirport { get; set; }

        [JsonPropertyName("arrivalAirport")]
        public ArrivalAirport ArrivalAirport { get; set; }

        [JsonPropertyName("path")]
        public List<string> Path { get; } = new List<string>();

        [JsonPropertyName("duration")]
        public Duration Duration { get; set; }

        [JsonPropertyName("stopSummary")]
        public StopSummary StopSummary { get; set; }

        [JsonPropertyName("totals")]
        public Totals Totals { get; set; }
    }

    public class FlightFare
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("searchData")]
        public SearchData SearchData { get; set; }

        [JsonPropertyName("results")]
        public List<Result> Results { get; } = new List<Result>();
    }

    /// <summary>
    /// Searching filters
    /// </summary>
    public class SearchData
    {
        [JsonPropertyName("from")]
        public string? From { get; set; }

        [JsonPropertyName("to")]
        public string? To { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("adult")]
        public int? Adult { get; set; }

        [JsonPropertyName("child")]
        public int? Child { get; set; }

        [JsonPropertyName("infant")]
        public int? Infant { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }
    }

    public class StopSummary
    {
        [JsonPropertyName("0")]
        public _0 _0 { get; set; }

        [JsonPropertyName("1")]
        public _1 _1 { get; set; }
    }

    public class Totals
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("baggage")]
        public object Baggage { get; set; }

        [JsonPropertyName("penalty")]
        public object Penalty { get; set; }

        [JsonPropertyName("total")]
        public double Total { get; set; }

        [JsonPropertyName("tax")]
        public double Tax { get; set; }

        [JsonPropertyName("base")]
        public double Base { get; set; }
    }

