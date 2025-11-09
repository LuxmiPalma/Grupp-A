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

    public async Task<List<Session>> GetMyBookingsAsync(int userId)
    {
        return await _repository.GetUserBookingsAsync(userId);
    }

    public async Task<(bool success, string message)> BookSessionAsync(int userId, int sessionId)
    {
        var (valid, message) = await ValidateAsync(userId, sessionId);
        if (!valid)
            return (false, message);

        var success = await _repository.BookSessionAsync(userId, sessionId);
        return (success, success ? "You have successfully booked the session." : "Failed to book the session. Please try again.");
    }

    public async Task<(bool success, string message)> CancelBookingAsync(int userId, int sessionId)
    {
        var success = await _repository.CancelBookingAsync(userId, sessionId);
        return (success, success ? "The booking has been cancelled." : "Failed to cancel the booking.");
    }

    public async Task<bool> IsBookedAsync(int userId, int sessionId)
    {
        return await _repository.IsBookedAsync(userId, sessionId);
    }

    public async Task<(bool valid, string message)> ValidateAsync(int userId, int sessionId)
    {
        var result = await _repository.ValidateBookingAsync(userId, sessionId);
        return (result.IsValid, result.Message);
    }
}