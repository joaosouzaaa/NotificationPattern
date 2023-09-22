using NotificationPattern.Settings.DatabaseSettings;

namespace NotificationPattern.DependencyInjection;

public static class DependencyInjectionHandler
{
    public static void AddDependencyInjectionHandler(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositoryDependencyInjection();
        services.AddFiltersDependencyInjection();
        services.AddSettingsDependencyInjection();
        DatabaseFactory.CreateDatabase(configuration.GetConnectionString("InitialConnection"));
    }
}
