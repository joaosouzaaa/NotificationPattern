﻿using Dapper;
using Domain.Entities;
using Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infra.Repositories;

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
            person.Name
        };

        var addPersonResult = await sqlConnection.ExecuteAsync(addPersonSqlString, personToAdd);

        return addPersonResult > 0;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        const string getAllSqlString = @"SELECT *
            FROM PERSON";

        return await sqlConnection.QueryAsync<Person>(getAllSqlString);
    }
}