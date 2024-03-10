using System.Reflection;
using AviaSales.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AviaSales.Persistence;

/// <summary>
/// Represents the database context for the AviaSales application.
/// </summary>
public class AviaSalesDb : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AviaSalesDb"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the context.</param>
    public AviaSalesDb (DbContextOptions<AviaSalesDb> options) : base(options){ }
    
    public DbSet<Airline> Airlines { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    /// <summary>
    /// Overrides the base method to configure the database model using entity configurations from the current assembly.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}