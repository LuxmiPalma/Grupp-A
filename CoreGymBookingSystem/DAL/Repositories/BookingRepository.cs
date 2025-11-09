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

    /// <summary>
    /// Get all bookings made by a user.
    /// </summary>
    public async Task<List<Booking>> GetUserBookingsAsync(int userId)
    {
        return await _context.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.Session!)
                .ThenInclude(s => s.Instructor)
            .OrderBy(b => b.Session!.StartTime)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific booking by user and session.
    /// </summary>
    public async Task<Booking?> GetBookingAsync(int userId, int sessionId)
    {
        return await _context.Bookings
            .Where(b => b.UserId == userId && b.SessionId == sessionId)
            .Include(b => b.Session)
                .ThenInclude(s => s!.Instructor)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Check if a user has booked a specific session.
    /// </summary>
    public async Task<bool> IsBookedAsync(int userId, int sessionId)
    {
        return await _context.Bookings
            .AnyAsync(b => b.UserId == userId && b.SessionId == sessionId);
    }

    /// <summary>
    /// Book a session for a user.
    /// </summary>
    public async Task<bool> BookSessionAsync(int userId, int sessionId)
    {
        try
        {
            // Check if user exists
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            // Check if session exists
            var session = await _context.Sessions.FindAsync(sessionId);
            if (session == null)
                return false;

            // Create new booking
            var booking = new Booking
            {
                UserId = userId,
                SessionId = sessionId,
                BookingDate = DateTime.UtcNow,
                Status = "Confirmed"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Cancel a booking.
    /// </summary>
    public async Task<bool> CancelBookingAsync(int userId, int sessionId)
    {
        try
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.UserId == userId && b.SessionId == sessionId);

            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validate if a booking is possible.
    /// </summary>
    public async Task<BookingValidationResult> ValidateBookingAsync(int userId, int sessionId)
    {
        // Check if user exists
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return new BookingValidationResult(false, "User not found");

        // Check if session exists
        var session = await _context.Sessions
            .Include(s => s.Bookings)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session == null)
            return new BookingValidationResult(false, "Session not found");

        // Check if session is full
        if (session.Bookings.Count >= session.MaxParticipants)
            return new BookingValidationResult(false,
                $"The session is full ({session.Bookings.Count}/{session.MaxParticipants})");

        // Check if already booked
        if (await IsBookedAsync(userId, sessionId))
            return new BookingValidationResult(false, "You have already booked this session");

        // Check if session has started
        if (session.StartTime <= DateTime.UtcNow)
            return new BookingValidationResult(false, "Cannot book a session that has already started");

        return new BookingValidationResult(true, "OK");
    }
}