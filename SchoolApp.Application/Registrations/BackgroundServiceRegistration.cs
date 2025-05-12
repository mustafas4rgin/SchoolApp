using Microsoft.Extensions.DependencyInjection;
using SchoolApp.Application.BackgroundServices;

namespace SchoolApp.Application.Registrations;

public static class BackgroundServiceRegistration
{
    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<TokenCleanupService>();

        return services;
    }
}