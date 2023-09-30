using Domain.Entities;
using Domain.Errors;
using Domain.Validators;

namespace UnitTests.ValidatorTests;
public sealed class PersonValidatorTests
{
    private readonly PersonValidator _personValidator;

	public PersonValidatorTests()
	{
		_personValidator = new PersonValidator();
	}

	[Fact]
	public void IsPersonValid_PersonIsValid_ReturnsTrue()
	{
		// A
		var person = new Person()
		{
			Name = "Test",
			Age = 30
		};

		// A
		var isPersonValidResult = _personValidator.IsPersonValid(person);

		// A
		Assert.Empty(isPersonValidResult.Errors);
		Assert.True(isPersonValidResult.IsValid);
	}

    [Fact]
    public void IsPersonValid_NameIsInvalid_ReturnsFalse()
    {
        // A
        var person = new Person()
        {
            Name = new string('a', 60),
			Age = 10
        };

        // A
        var isPersonValidResult = _personValidator.IsPersonValid(person);

        // A
        Assert.True(isPersonValidResult.Errors.ContainsKey(InvalidPersonErrors.InvalidNameError.Key));
        Assert.False(isPersonValidResult.IsValid);
    }

    [Fact]
    public void IsPersonValid_AgeIsInvalid_ReturnsFalse()
    {
        // A
        var person = new Person()
        {
            Name = "test",
            Age = -10
        };

        // A
        var isPersonValidResult = _personValidator.IsPersonValid(person);

        // A
        Assert.True(isPersonValidResult.Errors.ContainsKey(InvalidPersonErrors.InvalidAgeError.Key));
        Assert.False(isPersonValidResult.IsValid);
    }
}
