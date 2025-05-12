using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolApp.Applicaiton.Providers.Service;
using SchoolApp.Application.Helpers;

namespace SchoolApp.Application.Registrations;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services,IConfiguration configuration)
    {
        services.ValidatorAssembler();

        services.AddBackgroundServices();

        UniverstiyInformationHelper.Initialize(configuration);

        services.AddAuthService(configuration);

        ServiceRegistrationProvider.RegisterServices(services);
        
        return services;
    }
}