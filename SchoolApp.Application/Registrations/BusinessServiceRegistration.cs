using Microsoft.Extensions.DependencyInjection;
using SchoolApp.Applicaiton.Providers.Service;

namespace SchoolApp.Application.Registrations;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.ValidatorAssembler();

        ServiceRegistrationProvider.RegisterServices(services);
        
        return services;
    }
}