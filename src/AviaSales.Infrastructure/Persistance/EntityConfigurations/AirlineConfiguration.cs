using AviaSales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviaSales.Infrastructure.Persistance.EntityConfigurations;

/// <summary>
/// Configuration class for the <see cref="Airline"/> entity.
/// </summary>
public class AirlineConfiguration : IEntityTypeConfiguration<Airline>
{
    /// <summary>
    /// Configures the entity mapping for the <see cref="Airline"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Airline> builder)
    {
        // Configures the primary key for the Airline entity.
        builder.HasKey(a => a.Id);

        // Configures the one-to-many relationship with the Flight entity.
        builder.HasMany(a => a.Flights)
            .WithOne(f => f.Airline)
            .HasForeignKey(f => f.AirlineId);

        // Configures a unique index on the Code property.
        builder.HasIndex(a => a.Code)
            .IsUnique();
        
        // Fixing space issue in database.
        builder.Property(a => a.Name).HasMaxLength(255);
        builder.Property(a => a.Code).HasMaxLength(20);
    }
}
