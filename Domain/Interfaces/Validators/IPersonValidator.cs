using Domain.Entities;

namespace Domain.Interfaces.Validators;
public interface IPersonValidator
{
    bool IsPersonValid(Person person);
}
