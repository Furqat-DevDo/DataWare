using AviaSales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviaSales.Persistence.EntityConfigurations;

/// <summary>
/// Configuration class for the <see cref="Passenger"/> entity.
/// </summary>
public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    /// <summary>
    /// Configures the entity mapping for the <see cref="Passenger"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        // Configures the one-to-one relationship with Flight.
        builder.HasOne(p => p.Flight)
            .WithOne()
            .HasForeignKey<Passenger>(p => p.FlightId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configures a unique index on the Email property.
        builder.HasIndex(p => p.Email)
            .IsUnique();

        // Configures the maximum length for the Fullname property.
        builder.Property(p => p.Fullname)
            .HasMaxLength(255);

        // Configures the maximum length for the Email property.
        builder.Property(p => p.Email)
            .HasMaxLength(150);

        // Configures the maximum length for the Phone property.
        builder.Property(p => p.Phone)
            .HasMaxLength(15);
    }
}
