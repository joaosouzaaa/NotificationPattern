using Domain.Entities;

namespace Infra.Interfaces;

public interface IPersonRepository
{
    Task<bool> AddPersonAsync(Person person);
}
