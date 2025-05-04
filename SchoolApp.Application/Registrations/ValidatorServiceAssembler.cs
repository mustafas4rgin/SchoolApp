using Microsoft.Extensions.DependencyInjection;

namespace SchoolApp.Application.Registrations;

public static class ValidatorServiceAssembler
{
    public static IServiceCollection ValidatorAssembler(this IServiceCollection services)
    {
        services.AddCreateDtoValidators();

        services.AddUpdateDtoValidators();

        services.AddEntityValidators();

        return services;
    }
}