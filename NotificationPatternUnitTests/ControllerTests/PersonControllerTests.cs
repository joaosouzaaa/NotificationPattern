using Domain.Entities;
using Domain.Errors;
using Infra.Interfaces;
using Moq;
using NotificationPattern.Controllers;
using NotificationPattern.Interfaces;

namespace NotificationPatternUnitTests.ControllerTests;
public sealed class PersonControllerTests
{
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly PersonController _personController;

    public PersonControllerTests()
    {
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _personController = new PersonController(_notificationHandlerMock.Object, _personRepositoryMock.Object);
    }

    [Fact]
    public async Task AddPersonAsync_SucessfullScenario()
    {
        // A
        var person = new Person()
        {
            Name = "random"
        };
        _personRepositoryMock.Setup(p => p.AddPersonAsync(It.IsAny<Person>())).ReturnsAsync(true);

        // A
        var addPersonResult = await _personController.AddPersonAsync(person);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _personRepositoryMock.Verify(p => p.AddPersonAsync(It.IsAny<Person>()), Times.Once());

        Assert.True(addPersonResult);
    }

    [Fact]
    public async Task AddPersonAsync_NameIsInvalid_NotificationIsAdded_ReturnsFalse()
    {
        // A
        var person = new Person()
        {
            Name = new string('a', 60)
        };

        // A
        var addPersonResult = await _personController.AddPersonAsync(person);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.Is<string>(n => n == InvalidPersonErrors.InvalidNameError.Key), 
            It.Is<string>(n => n == InvalidPersonErrors.InvalidNameError.Value)), Times.Once());
        _personRepositoryMock.Verify(p => p.AddPersonAsync(It.IsAny<Person>()), Times.Never());

        Assert.False(addPersonResult);
    }
}
