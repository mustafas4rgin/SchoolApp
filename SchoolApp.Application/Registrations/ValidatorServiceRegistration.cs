using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SchoolApp.Application.Providers.Validator;

namespace SchoolApp.Application.Registrations;
public static class ValidatorServiceRegistration
{
    public static IServiceCollection AddAuthDtoValidators(this IServiceCollection services)
    {
        var authDTOValidatorAssemblies = AuthDTOValidatorAssemblyProvider.GetValidatorAssemblies();

        foreach (var assemblyType in authDTOValidatorAssemblies)
            services.AddValidatorsFromAssemblyContaining(assemblyType);

        return services;
    }
    public static IServiceCollection AddCreateDtoValidators(this IServiceCollection services)
    {
        var createDTOValidatorAssemblies = CreateDTOValidatorAssemblyProvider.GetValidatorAssemblies();

        foreach (var assemblyType in createDTOValidatorAssemblies)
            services.AddValidatorsFromAssemblyContaining(assemblyType);

        return services;
    }
    public static IServiceCollection AddUpdateDtoValidators(this IServiceCollection services)
    {
        var updateDTOValidatorAssemblies = UpdateDTOValidatorAssemblyProvider.GetValidatorAssemblies();

        foreach (var assemblyType in updateDTOValidatorAssemblies)
            services.AddValidatorsFromAssemblyContaining(assemblyType);

        return services;
    }
    public static IServiceCollection AddEntityValidators(this IServiceCollection services)
    {
        var entityValidatorAssemblies = EntityValidatorAssemblyProvider.GetValidatorAssemblies();

        foreach (var assemblyType in entityValidatorAssemblies)
            services.AddValidatorsFromAssemblyContaining(assemblyType);

        return services;
    }
}