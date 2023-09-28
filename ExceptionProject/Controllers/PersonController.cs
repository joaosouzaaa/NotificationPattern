using Domain.Entities;
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
	public async Task<bool> AddPersonAsync([FromBody] Person person)
	{
		try
		{
			if (!_personValidator.IsPersonValid(person))
				throw new InvalidNameException();

			return await _personRepository.AddPersonAsync(person);
        }
		catch
		{
			throw;
		}
	}
}
