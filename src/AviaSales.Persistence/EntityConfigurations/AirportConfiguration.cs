using AviaSales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviaSales.Persistence.EntityConfigurations;

/// <summary>
/// Configuration class for the <see cref="Airport"/> entity.
/// </summary>
public class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    /// <summary>
    /// Configures the entity mapping for the <see cref="Airport"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        // Configures the unique code associated with the airport.
        builder.Property(a => a.Code)
            .IsRequired();

        // Configures the timezone abbreviation for the airport.
        builder.Property(a => a.TZ)
            .IsRequired();

        // Configures the full timezone name for the airport.
        builder.Property(a => a.TimeZone)
            .IsRequired();

        // Configures the type of the airport (e.g., international, domestic).
        builder.Property(a => a.Type)
            .IsRequired();

        // Configures the label or name associated with the airport.
        builder.Property(a => a.Label)
            .IsRequired();

        // Configures the city where the airport is located.
        builder.Property(a => a.City)
            .IsRequired();
        
        // Configuring owned types.
        builder.OwnsOne(a => a.Details, d =>
        {
            d.Property(p => p.IataCode)
                .HasColumnName("IataCode")
                .HasMaxLength(4);
            
            d.Property(p => p.IcaoCode)
                .HasColumnName("IcaoCode")
                .HasMaxLength(6);
            
            d.Property(p => p.Facilities)
                .HasColumnName("Facilities")
                .HasMaxLength(400);
        });
        
        // Configuring owned types.
        builder.OwnsOne(a => a.Location, l =>
        {
            l.Property(p => p.Longitude).HasColumnName("Longtitude");
            l.Property(p => p.Latitude).HasColumnName("Latitude");
            l.Property(p => p.Elevation).HasColumnName("Elevation");
        });
    }
}
