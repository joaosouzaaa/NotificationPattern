using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Validators;
using Domain.Validators;
using Infra.Interfaces;
using Moq;
using NotificationPattern.Controllers;
using NotificationPattern.Interfaces;

namespace UnitTests.NotificationPatternUnitTests.ControllerTests;
public sealed class PersonControllerTests
{
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IPersonValidator> _personValidatorMock;
    private readonly PersonController _personController;

    public PersonControllerTests()
    {
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _personValidatorMock = new Mock<IPersonValidator>();
        _personController = new PersonController(_notificationHandlerMock.Object, _personRepositoryMock.Object, _personValidatorMock.Object);
    }

    [Fact]
    public async Task AddPersonAsync_SucessfullScenario()
    {
        // A
        var person = new Person()
        {
            Name = "random",
            Age = 123
        };
        var validationResult = new ValidationResult()
        {
            IsValid = true
        };
        _personValidatorMock.Setup(p => p.IsPersonValid(It.IsAny<Person>())).Returns(validationResult);
        _personRepositoryMock.Setup(p => p.AddPersonAsync(It.IsAny<Person>())).ReturnsAsync(true);

        // A
        var addPersonResult = await _personController.AddPersonAsync(person);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _personRepositoryMock.Verify(p => p.AddPersonAsync(It.IsAny<Person>()), Times.Once());

        Assert.True(addPersonResult);
    }

    [Fact]
    public async Task AddPersonAsync_PersonIsInvalid_NotificationIsAdded_ReturnsFalse()
    {
        // A
        var invalidPerson = new Person()
        {
            Name = new string('a', 60),
            Age = 10
        };
        var validationResult = new ValidationResult()
        {
            IsValid = false,
            Errors = new Dictionary<string, string>()
            {
                { "Name", "test" },
                { "age", "random" },
            }
        };
        _personValidatorMock.Setup(p => p.IsPersonValid(It.IsAny<Person>())).Returns(validationResult);

        // A
        var addPersonResult = await _personController.AddPersonAsync(invalidPerson);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _personRepositoryMock.Verify(p => p.AddPersonAsync(It.IsAny<Person>()), Times.Never());

        Assert.False(addPersonResult);
    }
}
