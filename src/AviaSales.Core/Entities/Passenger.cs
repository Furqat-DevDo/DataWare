using AviaSales.Shared.Entities;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents a passenger entity with information about their details and associated flight.
/// </summary>
public class Passenger : Entity<long>
{
    // Private parameterless constructor to prevent direct instantiation.
    private Passenger() { }

    /// <summary>
    /// Gets or sets the optional user ID associated with the passenger.
    /// </summary>
    public long? UserId { get; private set; }

    /// <summary>
    /// Gets or sets the email of the passenger.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets or sets the phone number of the passenger.
    /// </summary>
    public string Phone { get; private set; }

    /// <summary>
    /// Gets or sets the full name of the passenger.
    /// </summary>
    public string Fullname { get; private set; }

    /// <summary>
    /// used for acsess pessanger bookings.
    /// </summary>
    public IReadOnlyList<Booking> Bookings { get; set; }
    
    /// <summary>
    /// Creates a new passenger entity with the specified details.
    /// </summary>
    /// <param name="userId">The optional user ID associated with the passenger.</param>
    /// <param name="email">The email of the passenger.</param>
    /// <param name="phone">The phone number of the passenger.</param>
    /// <param name="fullname">The full name of the passenger.</param>
    /// <returns>A new instance of the <see cref="Passenger"/> class.</returns>
    public static Passenger Create(long? userId, string email, string phone, string fullname)
    {
        var passenger = new Passenger
        {
            UserId = userId,
            Email = email,
            Phone = phone,
            Fullname = fullname,
            CreatedAt = DateTime.UtcNow
        };

        return passenger;
    }

    /// <summary>
    /// Updates user information for a specific flight.
    /// </summary>
    /// <param name="userId">The unique identifier of the user (optional). Pass null if not applicable.</param>
    /// <param name="phone">The updated phone number associated with the user.</param>
    /// <param name="email">The updated email address associated with the user.</param>
    /// <param name="fullname">The updated full name of the user.</param>
    public void Update(long? userId,string phone, string email, string fullname)
    {
        UserId = userId;
        Phone = phone;
        Email = email;
        Fullname = fullname;
        UpdatedAt = DateTime.UtcNow;
    }
}
