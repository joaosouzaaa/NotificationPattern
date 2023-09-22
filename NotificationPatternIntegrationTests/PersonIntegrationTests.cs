using Domain.Entities;
using NotificationPatternIntegrationTests.Fixture;
using System.Net.Http.Json;

namespace NotificationPatternIntegrationTests;
public sealed class PersonIntegrationTests : HttpClientFixture
{
    public PersonIntegrationTests(ContainerFixture containerFixture) : base(containerFixture)
    {
    }

    [Fact]
    public async Task AddPerson_SuccessfullScenario_Returns200Ok()
    {
        // A
        var personToAdd = new Person()
        {
            Name = "Test"
        };
        const string addPersonRequestUri = "api/Person/add-person";

        // A
        var addPersonHttpResponseMessageResult = await _httpClient.PostAsJsonAsync(addPersonRequestUri, personToAdd);

        var rm = addPersonHttpResponseMessageResult.StatusCode;
        var rn = await addPersonHttpResponseMessageResult.Content.ReadAsStringAsync();
        // A
    }
}
