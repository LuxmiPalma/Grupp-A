using DAL.Entities;
using DAL.Repositories.Interfaces;
using Service.Interfaces;

namespace Service.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;

    public BookingService(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Get all sessions booked by a user (with session details).
    /// </summary>
    public async Task<List<Session>> GetMyBookingsAsync(int userId)
    {
        // Get bookings from repository
        var bookings = await _repository.GetUserBookingsAsync(userId);

        // Convert to Sessions (extract the Session from each Booking)
        return bookings
            .Select(b => b.Session!)  // ← Select the Session from Booking
            .ToList();
    }

    /// <summary>
    /// Book a session for a user.
    /// </summary>
    public async Task<(bool success, string message)> BookSessionAsync(int userId, int sessionId)
    {
        // Validate first
        var validation = await _repository.ValidateBookingAsync(userId, sessionId);
        if (!validation.IsValid)
            return (false, validation.Message);

        // Try to book
        var success = await _repository.BookSessionAsync(userId, sessionId);

        if (success)
            return (true, "Bokning lyckades!");
        else
            return (false, "Bokning misslyckades - försök igen senare");
    }

    /// <summary>
    /// Cancel a booking.
    /// </summary>
    public async Task<(bool success, string message)> CancelBookingAsync(int userId, int sessionId)
    {
        var success = await _repository.CancelBookingAsync(userId, sessionId);

        if (success)
            return (true, "Bokning avbokad!");
        else
            return (false, "Avbokning misslyckades - försök igen senare");
    }

    /// <summary>
    /// Check if a user has booked a specific session.
    /// </summary>
    public async Task<bool> IsBookedAsync(int userId, int sessionId)
    {
        return await _repository.IsBookedAsync(userId, sessionId);
    }

    /// <summary>
    /// Validate if a booking is possible.
    /// </summary>
    public async Task<(bool valid, string message)> ValidateAsync(int userId, int sessionId)
    {
        var result = await _repository.ValidateBookingAsync(userId, sessionId);
        return (result.IsValid, result.Message);
    }
}