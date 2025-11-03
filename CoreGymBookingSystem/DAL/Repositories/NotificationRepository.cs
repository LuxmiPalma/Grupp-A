using DAL.DbContext;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext _context;

    public NotificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Notification>> GetAllAsync()
    {
        return await _context.Notifications
            .Include(n => n.Recipient)
            .ToListAsync();
    }

    public async Task<List<Notification>> GetByRecipientIdAsync(int recipientId)
    {
        return await _context.Notifications
            .Where(n => n.RecipientId == recipientId)
            .Include(n => n.Recipient)
            .OrderByDescending(n => n.Id)
            .ToListAsync();
    }

    public async Task<Notification?> GetByIdAsync(int id)
    {
        return await _context.Notifications
            .Include(n => n.Recipient)
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
    }

    public async Task UpdateAsync(Notification notification)
    {
        _context.Notifications.Update(notification);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var notification = await GetByIdAsync(id);
        if (notification != null)
        {
            _context.Notifications.Remove(notification);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}