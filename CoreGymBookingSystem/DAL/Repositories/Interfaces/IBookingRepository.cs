using DAL.Entities;
using DAL.Models;

namespace DAL.Repositories.Interfaces;

public interface IBookingRepository
{
    /// <summary>
    /// Get all bookings made by a user.
    /// </summary>
    Task<List<Booking>> GetUserBookingsAsync(int userId);

    /// <summary>
    /// Get a specific booking by user and session.
    /// </summary>
    Task<Booking?> GetBookingAsync(int userId, int sessionId);

    /// <summary>
    /// Check if a user has booked a specific session.
    /// </summary>
    Task<bool> IsBookedAsync(int userId, int sessionId);

    /// <summary>
    /// Book a session for a user.
    /// </summary>
    Task<bool> BookSessionAsync(int userId, int sessionId);

    /// <summary>
    /// Cancel a booking.
    /// </summary>
    Task<bool> CancelBookingAsync(int userId, int sessionId);

    /// <summary>
    /// Validate if a booking is possible.
    /// </summary>
    Task<BookingValidationResult> ValidateBookingAsync(int userId, int sessionId);
}