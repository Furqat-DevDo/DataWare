namespace AviaSales.External.Services.Models;

public class FakeFilters
{
    public DateTime? DateFrom { get; set; }
    public int? Transactions { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
}
public class FakeFlight
{
    public long Id { get; set; }
    public string? ExternalId { get; set; }
    public long AirlineId { get; set; }
    public long DepartureAirportId { get; set; }
    public DateTime DepartureTime { get; set; }
    public long ArrivalAirportId { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int PassengersCount { get; set; }
    public int Transactions  { get; set; }
    public bool  HasFreeBagage { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    
    public static IEnumerable<FakeFlight> GenerateFakeFlights(int count)
    {
        var random = new Random();

        return Enumerable.Range(1, count).Select(_=> new FakeFlight
        {
            Id = -_,
            ExternalId = $"ExtId{-_}",
            AirlineId = random.Next(1, 10),
            DepartureAirportId = random.Next(100, 200),
            DepartureTime = DateTime.UtcNow.AddHours(random.Next(-24, 24)),
            ArrivalAirportId = random.Next(200, 300),
            ArrivalTime = DateTime.UtcNow.AddHours(random.Next(24, 48)),
            PassengersCount = random.Next(50, 200),
            Transactions = random.Next(10, 50),
            HasFreeBagage = random.Next(2) == 1, // 50% chance of true
            Price = Math.Round((decimal)random.NextDouble() * 1000, 2),
            IsAvailable = random.Next(2) == 1 // 50% chance of true
        });
    }
}