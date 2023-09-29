using Domain.Entities;
using Domain.Validators;

namespace Domain.Interfaces.Validators;
public interface IPersonValidator
{
    ValidationResult IsPersonValid(Person person);
}
