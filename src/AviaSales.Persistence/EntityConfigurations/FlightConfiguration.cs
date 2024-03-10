using AviaSales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviaSales.Persistence.EntityConfigurations;

/// <summary>
/// Configuration class for the <see cref="Flight"/> entity.
/// </summary>
public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    /// <summary>
    /// Configures the entity mapping for the <see cref="Flight"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        // Configures the relationship with the Airline entity.
        builder.HasOne(f => f.Airline)
            .WithMany(a => a.Flights)
            .HasForeignKey(f => f.AirlineId);

        // Configures the relationship with the DepartureAirport entity.
        builder.HasOne(f => f.DepartureAirport)
            .WithMany()
            .HasForeignKey(f => f.DepartureAirportId);

        // Configures the relationship with the ArrivalAirport entity.
        builder.HasOne(f => f.ArrivalAirport)
            .WithMany()
            .HasForeignKey(f => f.ArrivalAirportId);
        
        // Configures Passenger and Flight Relation Ship
        builder.HasOne(f => f.Passenger)
            .WithOne(p => p.Flight)
            .HasForeignKey<Flight>(f => f.PassengerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.OwnsMany(f => f.Prices, pr =>
        {
            pr.Property(p => p.Amount).HasColumnName("Amount");
            pr.Property(p => p.Type)
                .HasColumnName("PriceType")
                .HasConversion<string>();
        });
    }
}
