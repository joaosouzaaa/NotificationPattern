using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Validators;
using ExceptionProject.Exceptions;
using Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepository;
	private readonly IPersonValidator _personValidator;

	public PersonController(IPersonRepository personRepository, IPersonValidator personValidator)
	{
		_personRepository = personRepository;
		_personValidator = personValidator;
	}

	[HttpPost("add-person")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<Notification>))]
    public async Task<bool> AddPersonAsync([FromBody] Person person)
	{
		try
		{
			var isPersonValid = _personValidator.IsPersonValid(person);

            if (!isPersonValid.IsValid)
			{
				if(isPersonValid.Errors.ContainsKey(InvalidPersonErrors.InvalidNameError.Key))
					throw new InvalidNameException();

				if (isPersonValid.Errors.ContainsKey(InvalidPersonErrors.InvalidAgeError.Key))
					throw new InvalidAgeException();
			}

			return await _personRepository.AddPersonAsync(person);
        }
		catch
		{
			throw;
		}
	}
}
