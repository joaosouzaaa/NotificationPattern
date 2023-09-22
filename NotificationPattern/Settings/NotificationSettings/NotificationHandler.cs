using Domain.Errors;
using NotificationPattern.Interfaces;

namespace NotificationPattern.Settings.NotificationSettings;

public sealed class NotificationHandler : INotificationHandler
{
    private readonly List<Notification> _notificationList;

    public NotificationHandler()
    {
        _notificationList = new List<Notification>();
    }

    public List<Notification> GetNotifications() =>
        _notificationList;

    public bool HasNotification() =>
        _notificationList.Any();

    public bool AddNotification(Notification notification)
    {
        _notificationList.Add(notification);
        
        return false;
    }

    public bool AddNotification(string key, string message)
    {
        var notification = new Notification()
        {
            Key = key,
            Message = message
        };

        _notificationList.Add(notification);

        return false;
    }
}
