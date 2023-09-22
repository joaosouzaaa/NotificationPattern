using Bogus;
using NotificationPattern.Settings.NotificationSettings;

namespace NotificationPatternUnitTests.SettingTests;
public sealed class NotificationHandlerTests
{
    private readonly NotificationHandler _notificationHandler;
	private readonly Randomizer _random;

    public NotificationHandlerTests()
	{
		_notificationHandler = new NotificationHandler();
		_random = new Faker().Random;
    }

	[Fact]
	public void GetNotifications_AddNotifications_ListHasNotifications()
	{
		// A
		const int notificationCount = 2;
		AddNotificationsInRange(notificationCount);

		// A
		var notificationListResult = _notificationHandler.GetNotifications();

        // A
		Assert.Equal(notificationListResult.Count, notificationCount);
    }

	[Fact]
	public void HasNotification_AddNotification_HasNotificationTrue()
	{
		// A
		var notificationToAdd = new DomainNotification()
		{
			Key = _random.Word(),
			Message = _random.Word(),
		};
		_notificationHandler.AddNotification(notificationToAdd);

		// A
		var hasNotificationResult = _notificationHandler.HasNotification();

		// A
		Assert.True(hasNotificationResult);
	}

    [Fact]
    public void HasNotification_HasNotificationFalse()
    {
        var hasNotificationResult = _notificationHandler.HasNotification();

        Assert.False(hasNotificationResult);
    }

	[Fact]
	public void AddNotification_AddEntity_ReturnsFalse()
	{
		// A
		var notificationToAdd = new DomainNotification()
		{
			Key = _random.Word(),
			Message = _random.Word()
		};

		// A
		var addNotificationResult = _notificationHandler.AddNotification(notificationToAdd);

		// A
		Assert.False(addNotificationResult);
	}

    [Fact]
    public void AddNotification_AddVariables_ReturnsFalse()
    {
		// A
		var key = _random.Word();
		var message = _random.Word();

        // A
        var addNotificationResult = _notificationHandler.AddNotification(key, message);

        // A
        Assert.False(addNotificationResult);
    }

    private void AddNotificationsInRange(int range)
	{
		for(var i = 0; i < range; i++)
		{
            var notification = new DomainNotification()
            {
                Key = _random.Word(),
                Message = _random.Word()
            };
            _notificationHandler.AddNotification(notification);
        }
    }
}
