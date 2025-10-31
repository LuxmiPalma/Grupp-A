using DAL.Entities;
using DAL.Repositories.Interfaces;
using Services.Interfaces;

namespace Service.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<List<Notification>> GetAllNotificationsAsync()
    {
        return await _notificationRepository.GetAllAsync();
    }

    public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
    {
        return await _notificationRepository.GetByRecipientIdAsync(userId);
    }

    public async Task<Notification?> GetNotificationByIdAsync(int id)
    {
        return await _notificationRepository.GetByIdAsync(id);
    }

    public async Task SendNotificationAsync(int recipientId, string title, string message)
    {
        var notification = new Notification
        {
            RecipientId = recipientId,
            Title = title,
            Message = message,
            Read = false
        };

        await _notificationRepository.AddAsync(notification);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task MarkAsReadAsync(int notificationId)
    {
        var notification = await _notificationRepository.GetByIdAsync(notificationId);
        if (notification != null)
        {
            notification.Read = true;
            await _notificationRepository.UpdateAsync(notification);
            await _notificationRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteNotificationAsync(int notificationId)
    {
        await _notificationRepository.DeleteAsync(notificationId);
        await _notificationRepository.SaveChangesAsync();
    }
}