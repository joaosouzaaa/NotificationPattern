using NotificationPattern.Settings.NotificationSettings;

namespace NotificationPattern.Interfaces.Settings;

public interface INotificationHandler
{
    public List<DomainNotification> GetNotifications();
    public bool HasNotification();
    public bool AddNotification(DomainNotification notification);
    public bool AddNotification(string key, string message);
}
