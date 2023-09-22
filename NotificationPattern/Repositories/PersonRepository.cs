using Dapper;
using NotificationPattern.Entities;
using NotificationPattern.Interfaces.Repositories;
using System.Data.SqlClient;

namespace NotificationPattern.Repositories;

public sealed class PersonRepository : IPersonRepository
{
    private readonly IConfiguration _configuration;

    public PersonRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> AddPersonAsync(Person person)
    {
        using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        
        const string addPersonSqlString = @"INSERT INTO PERSON (NAME) 
            VALUES (@Name)";

        var personToAdd = new
        {
            Name = person.Name
        };

        var addPersonResult = await sqlConnection.ExecuteAsync(addPersonSqlString, personToAdd);

        return addPersonResult > 0;
    }
}
