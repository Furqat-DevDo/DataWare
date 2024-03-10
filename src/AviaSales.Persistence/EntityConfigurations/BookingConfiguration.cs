using AviaSales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviaSales.Persistence.EntityConfigurations;

/// <summary>
/// Configuration class for the <see cref="Booking"/> entity.
/// </summary>
public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    /// <summary>
    /// Configures the entity mapping for the <see cref="Booking"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        // Configures the relationship with Flight.
        builder.HasOne(b => b.Flight)
            .WithMany(f => f.Bookings)
            .HasForeignKey(b => b.FlightId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configures the relationship with Passenger.
        builder.HasOne(b => b.Passenger)
            .WithMany(p => p.Bookings)
            .HasForeignKey(b => b.PassengerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configures the date and time when the booking was made.
        builder.Property(b => b.BookingDateTime)
            .IsRequired();

        // Configures the total price of the booking.
        builder.Property(b => b.TotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Configures the booking status.
        builder.Property(b => b.Status)
            .IsRequired()
            .HasConversion<string>(); 
        
        builder.HasIndex(b => new { b.FlightId, b.PassengerId }).IsUnique();
    }
}