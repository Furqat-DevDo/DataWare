﻿using AviaSales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviaSales.Infrastructure.Persistance.EntityConfigurations;

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
        // Configures the primary key for the Airport entity.
        builder.HasKey(a => a.Id);

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

        // Configures the country where the airport is situated.
        builder.Property(a => a.Country)
            .IsRequired();
    }
}