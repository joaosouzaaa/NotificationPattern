using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Validators;

namespace Domain.Validators;
public sealed class PersonValidator : IPersonValidator
{
    public ValidationResult IsPersonValid(Person person)
    {
        var validationResult = new ValidationResult()
        {
            IsValid = true
        };

        if(person.Name.Length > 50)
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add(InvalidPersonErrors.InvalidNameError.Key, InvalidPersonErrors.InvalidNameError.Value);
        }

        if(person.Age < 0)
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add(InvalidPersonErrors.InvalidAgeError.Key, InvalidPersonErrors.InvalidAgeError.Value);
        }

        return validationResult;
    }
}
