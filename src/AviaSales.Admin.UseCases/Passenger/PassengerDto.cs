namespace AviaSales.Admin.UseCases.Passenger;

/// <summary>
/// Data transfer object for representing passenger information.
/// </summary>
/// <param name="Id">The unique identifier of the passenger.</param>
/// <param name="UserId">The unique identifier of the associated user, if applicable.</param>
/// <param name="Email">The email address of the passenger.</param>
/// <param name="Phone">The phone number of the passenger.</param>
/// <param name="Fullname">The full name of the passenger.</param>
/// <param name="CreatedAt">The created time of passenger.</param>
public record PassengerDto(long Id, long? UserId, string Email, string Phone, string Fullname,DateTime CreatedAt);

/// <summary>
/// Data transfer object for creating a new passenger.
/// </summary>
/// <param name="UserId">The unique identifier of the associated user, if applicable.</param>
/// <param name="Email">The email address of the passenger.</param>
/// <param name="Phone">The phone number of the passenger.</param>
/// <param name="Fullname">The full name of the passenger.</param>
public record CreatePassengerDto(long? UserId, string Email, string Phone, string Fullname);

