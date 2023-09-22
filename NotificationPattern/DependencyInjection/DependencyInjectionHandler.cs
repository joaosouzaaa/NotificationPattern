using Infra.DependencyInjection;

namespace NotificationPattern.DependencyInjection;

public static class DependencyInjectionHandler
{
    public static void AddDependencyInjectionHandler(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFiltersDependencyInjection();
        services.AddSettingsDependencyInjection();
        services.AddInfraDependencyInjection(configuration); 
    }
}
