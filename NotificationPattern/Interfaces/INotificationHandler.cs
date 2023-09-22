using Domain.Errors;

namespace NotificationPattern.Interfaces;

public interface INotificationHandler
{
    public List<Notification> GetNotifications();
    public bool HasNotification();
    public bool AddNotification(Notification notification);
    public bool AddNotification(string key, string message);
}
