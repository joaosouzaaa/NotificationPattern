using Microsoft.AspNetCore.Mvc;
using NotificationPattern.Entities;
using NotificationPattern.Errors;
using NotificationPattern.Interfaces.Repositories;
using NotificationPattern.Interfaces.Settings;

namespace NotificationPattern.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class PersonController : ControllerBase
{
	private readonly INotificationHandler _notificationHandler;
	private readonly IPersonRepository _personRepository;

	public PersonController(INotificationHandler notificationHandler, IPersonRepository personRepository)
	{
		_notificationHandler = notificationHandler;
		_personRepository = personRepository;
	}

	[HttpPost("add-person")]
	public async Task<bool> AddPersonAsync([FromBody] Person person)
	{
		if (!IsNameValid(person.Name))
			return _notificationHandler.AddNotification(InvalidPersonErrors.InvalidNameError.Key, InvalidPersonErrors.InvalidNameError.Value);

		return await _personRepository.AddPersonAsync(person);
	}

    private bool IsNameValid(string name) => 
		name.Length < 50;
}
