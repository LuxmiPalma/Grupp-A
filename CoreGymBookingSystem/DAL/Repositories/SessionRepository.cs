using DAL.DbContext;
using DAL.Entitites;
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
        return await _context.Sessions.Include(s => s.Instructor).ToListAsync();
    }

    public async Task<Session?> GetByIdAsync(int id)
    {
        return await _context.Sessions
            .Include(s => s.Instructor)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(Session session)
    {
        await _context.Sessions.AddAsync(session);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
