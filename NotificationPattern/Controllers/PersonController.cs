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
		var isPersonValid = _personValidator.IsPersonValid(person);

        if (!isPersonValid.IsValid)
		{
			foreach(var error in isPersonValid.Errors)
			{
				_notificationHandler.AddNotification(error.Key, error.Value);
			}

			return isPersonValid.IsValid;
		}

		return await _personRepository.AddPersonAsync(person);
	}
}
