using NotificationPattern.Entities;

namespace NotificationPattern.Interfaces.Repositories;

public interface IPersonRepository
{
    Task<bool> AddPersonAsync(Person person);
}
