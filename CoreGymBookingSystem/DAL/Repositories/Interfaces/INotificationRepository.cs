using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface INotificationRepository
{
    Task<List<Notification>> GetAllAsync();
    Task<List<Notification>> GetByRecipientIdAsync(int recipientId);
    Task<Notification?> GetByIdAsync(int id);
    Task AddAsync(Notification notification);
    Task UpdateAsync(Notification notification);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}
