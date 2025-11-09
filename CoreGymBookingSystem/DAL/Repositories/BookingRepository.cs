using DAL.DbContext;
using DAL.Entities;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;
public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;

    public BookingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Session>> GetUserBookingsAsync(int userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.Bookings)
                .ThenInclude(s => s.Instructor)
            .SelectMany(u => u.Bookings)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<Session?> GetBookingAsync(int userId, int sessionId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.Bookings)
                .ThenInclude(s => s.Instructor)
            .SelectMany(u => u.Bookings)
            .FirstOrDefaultAsync(s => s.Id == sessionId);
    }

    public async Task<bool> IsBookedAsync(int userId, int sessionId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Bookings)
            .AnyAsync(s => s.Id == sessionId);
    }

    public async Task<bool> BookSessionAsync(int userId, int sessionId)
    {
        try
        {
            var user = await _context.Users
                .Include(u => u.Bookings)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var session = await _context.Sessions.FindAsync(sessionId);

            if (user == null || session == null)
                return false;

            user.Bookings.Add(session);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> CancelBookingAsync(int userId, int sessionId)
    {
        try
        {
            var user = await _context.Users
                .Include(u => u.Bookings)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            var session = user.Bookings.FirstOrDefault(s => s.Id == sessionId);
            if (session == null)
                return false;

            user.Bookings.Remove(session);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<BookingValidationResult> ValidateBookingAsync(int userId, int sessionId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return new BookingValidationResult(false, "User not found");

        var session = await _context.Sessions
            .Include(s => s.Bookings)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session == null)
            return new BookingValidationResult(false, "Session not found");

        if (session.Bookings.Count >= session.MaxParticipants)
            return new BookingValidationResult(false,
                $"The session is full ({session.Bookings.Count}/{session.MaxParticipants})");

        if (await IsBookedAsync(userId, sessionId))
            return new BookingValidationResult(false, "You have already booked this session");

        if (session.StartTime <= DateTime.UtcNow)
            return new BookingValidationResult(false, "Cannot book a session that has already started");

        return new BookingValidationResult(true, "OK");
    }
}