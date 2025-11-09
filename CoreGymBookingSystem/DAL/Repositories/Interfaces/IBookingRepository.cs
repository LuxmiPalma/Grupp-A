using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface IBookingRepository
{
    Task<List<Session>> GetUserBookingsAsync(int userId);
    Task<Session?> GetBookingAsync(int userId, int sessionId);
    Task<bool> IsBookedAsync(int userId, int sessionId);

    Task<bool> BookSessionAsync(int userId, int sessionId);
    Task<bool> CancelBookingAsync(int userId, int sessionId);

    Task<BookingValidationResult> ValidateBookingAsync(int userId, int sessionId);
}
