using Domain.Entities;
using ExceptionProject.Exceptions;
using Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepository;

	public PersonController(IPersonRepository personRepository)
	{
		_personRepository = personRepository;
	}

	[HttpPost("add-person")]
	public async Task<bool> AddPersonAsync([FromBody] Person person)
	{
		try
		{
			if (!IsNameValid(person.Name))
				throw new InvalidNameException();

			return await _personRepository.AddPersonAsync(person);
        }
		catch
		{
			throw;
		}
	}

    private bool IsNameValid(string name) =>
        name.Length < 50;
}
