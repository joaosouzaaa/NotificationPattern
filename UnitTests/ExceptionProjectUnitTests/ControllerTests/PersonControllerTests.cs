using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Validators;
using Domain.Validators;
using ExceptionProject.Controllers;
using ExceptionProject.Exceptions;
using Infra.Interfaces;
using Moq;

namespace UnitTests.ExceptionProjectUnitTests.ControllerTests;
public sealed class PersonControllerTests
{
    private readonly Mock<IPersonRepository> _personRepository;
    private readonly Mock<IPersonValidator> _personValidatorMock;
    private readonly PersonController _personController;

    public PersonControllerTests()
    {
        _personRepository = new Mock<IPersonRepository>();
        _personValidatorMock = new Mock<IPersonValidator>();
        _personController = new PersonController(_personRepository.Object, _personValidatorMock.Object);
    }

    [Fact]
    public async Task AddPersonAsync_SuccessfullScenario()
    {
        // A
        var person = new Person()
        {
            Name = "Test",
            Age = 20
        };
        var validationResult = new ValidationResult()
        {
            IsValid = true
        };
        _personValidatorMock.Setup(p => p.IsPersonValid(It.IsAny<Person>())).Returns(validationResult);
        _personRepository.Setup(p => p.AddPersonAsync(It.IsAny<Person>())).ReturnsAsync(true);

        // A
        var addPersonResult = await _personController.AddPersonAsync(person);

        // A
        _personRepository.Verify(p => p.AddPersonAsync(It.IsAny<Person>()), Times.Once());

        Assert.True(addPersonResult);
    }

    [Fact]
    public async Task AddPersonAsync_NameIsInvalid_ExceptionIsThrowed()
    {
        // A
        var invalidPerson = new Person()
        {
            Name = new string('a', 60),
            Age = 20
        };
        var validationResult = new ValidationResult()
        {
            IsValid = false,
            Errors = new Dictionary<string, string>()
            {
                {InvalidPersonErrors.InvalidNameError.Key, InvalidPersonErrors.InvalidNameError.Value }
            }
        };
        _personValidatorMock.Setup(p => p.IsPersonValid(It.IsAny<Person>())).Returns(validationResult);

        // A
        var addPersonResult = async () => await _personController.AddPersonAsync(invalidPerson);

        // A
        _personRepository.Verify(p => p.AddPersonAsync(It.IsAny<Person>()), Times.Never());

        Assert.ThrowsAsync<InvalidNameException>(addPersonResult);
    }

    [Fact]
    public async Task AddPersonAsync_AgeIsInvalid_ExceptionIsThrowed()
    {
        // A
        var invalidPerson = new Person()
        {
            Name = "test",
            Age = -20
        };
        var validationResult = new ValidationResult()
        {
            IsValid = false,
            Errors = new Dictionary<string, string>()
            {
                {InvalidPersonErrors.InvalidAgeError.Key, InvalidPersonErrors.InvalidAgeError.Value }
            }
        };
        _personValidatorMock.Setup(p => p.IsPersonValid(It.IsAny<Person>())).Returns(validationResult);

        // A
        var addPersonResult = async () => await _personController.AddPersonAsync(invalidPerson);

        // A
        _personRepository.Verify(p => p.AddPersonAsync(It.IsAny<Person>()), Times.Never());

        Assert.ThrowsAsync<InvalidAgeException>(addPersonResult);
    }
}
