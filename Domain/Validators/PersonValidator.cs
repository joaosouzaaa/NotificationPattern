using Domain.Entities;
using Domain.Interfaces.Validators;

namespace Domain.Validators;
public sealed class PersonValidator : IPersonValidator
{
    public bool IsPersonValid(Person person) =>
        person.Name.Length < 50;
}
