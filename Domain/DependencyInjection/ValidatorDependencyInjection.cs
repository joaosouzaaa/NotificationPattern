using Domain.Interfaces.Validators;
using Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.DependencyInjection;
public static class ValidatorDependencyInjection
{
    public static void AddValidatorDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPersonValidator, PersonValidator>();
    }
}
