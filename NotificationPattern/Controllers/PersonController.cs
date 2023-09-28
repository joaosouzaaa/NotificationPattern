using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Validators;
using Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NotificationPattern.Interfaces;

namespace NotificationPattern.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class PersonController : ControllerBase
{
	private readonly INotificationHandler _notificationHandler;
	private readonly IPersonRepository _personRepository;
	private readonly IPersonValidator _personValidator;

	public PersonController(INotificationHandler notificationHandler, IPersonRepository personRepository,
                            IPersonValidator personValidator)
	{
		_notificationHandler = notificationHandler;
		_personRepository = personRepository;
		_personValidator = personValidator;
	}

	[HttpPost("add-person")]
	public async Task<bool> AddPersonAsync([FromBody] Person person)
	{
		if (!_personValidator.IsPersonValid(person))
			return _notificationHandler.AddNotification(InvalidPersonErrors.InvalidNameError.Key, InvalidPersonErrors.InvalidNameError.Value);

		return await _personRepository.AddPersonAsync(person);
	}
}
