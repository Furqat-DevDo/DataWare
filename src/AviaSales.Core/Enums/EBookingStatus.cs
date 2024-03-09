namespace AviaSales.Core.Enums;

/// <summary>
/// Represents the status of a booking.
/// </summary>
public enum EBookingStatus
{
    /// <summary>
    /// The booking has been confirmed.
    /// </summary>
    Confirmed,

    /// <summary>
    /// The booking is pending and awaiting confirmation.
    /// </summary>
    Pending,

    /// <summary>
    /// The booking has been cancelled.
    /// </summary>
    Cancelled,

    /// <summary>
    /// The booking is in progress or being processed.
    /// </summary>
    InProgress
}