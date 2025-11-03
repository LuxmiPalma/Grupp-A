using DAL.Entities;

namespace Services.Interfaces;

public interface INotificationService
{
    Task<List<Notification>> GetAllNotificationsAsync();
    Task<List<Notification>> GetUserNotificationsAsync(int userId);
    Task<Notification?> GetNotificationByIdAsync(int id);
    Task SendNotificationAsync(int recipientId, string title, string message);
    Task MarkAsReadAsync(int notificationId);
    Task DeleteNotificationAsync(int notificationId);
}
