using NotificationPattern.Interfaces.Repositories;
using NotificationPattern.Repositories;

namespace NotificationPattern.DependencyInjection;

public static class RepositoryDependencyInjection
{
    public static void AddRepositoryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();
    }
}
