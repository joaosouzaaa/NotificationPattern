using Infra.Interfaces;
using Infra.Repositories;
using Infra.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.DependencyInjection;

public static class InfraDependencyInjection
{
    public static void AddInfraDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();

        DatabaseFactory.CreateDatabase(configuration.GetConnectionString("InitialConnection"));
    }
}
