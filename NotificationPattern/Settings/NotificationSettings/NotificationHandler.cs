using NotificationPattern.Interfaces.Settings;

namespace NotificationPattern.Settings.NotificationSettings;

public sealed class NotificationHandler : INotificationHandler
{
    private readonly List<DomainNotification> _notificationList;

    public NotificationHandler()
    {
        _notificationList = new List<DomainNotification>();
    }

    public List<DomainNotification> GetNotifications() =>
        _notificationList;

    public bool HasNotification() =>
        _notificationList.Any();

    public bool AddNotification(DomainNotification notification)
    {
        _notificationList.Add(notification);
        
        return false;
    }

    public bool AddNotification(string key, string message)
    {
        var notification = new DomainNotification()
        {
            Key = key,
            Message = message
        };

        _notificationList.Add(notification);

        return false;
    }
}
