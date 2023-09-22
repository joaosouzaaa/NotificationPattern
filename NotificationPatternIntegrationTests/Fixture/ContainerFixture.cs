using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace NotificationPatternIntegrationTests.Fixture;
public sealed class ContainerFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer;

    public ContainerFixture()
    {
        _dbContainer = new MsSqlBuilder().WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .WithPortBinding(8080, true)
            .WithEnvironment("-e", "MSSQL_PID=Express")
            .WithName("SqlServer-IntegrationTest")
            .WithVolumeMount("./Infra/DatabaseScripts", "/app/infra")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync() =>
        await _dbContainer.StopAsync();
}
