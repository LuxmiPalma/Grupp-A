using DAL.DbContext;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly ApplicationDbContext _context;

    public SessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Session>> GetAllAsync()
    {
        return await _context.Sessions
            .Include(s => s.Bookings)           // Laddar bokningar
            .Include(s => s.Instructor)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<Session?> GetByIdAsync(int id)
    {
        return await _context.Sessions
            .Include(s => s.Bookings)           // Viktigt för SessionDetails
            .Include(s => s.Instructor)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}