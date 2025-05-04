using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolApp.Data.Contexts;
using SchoolApp.Data.Repositories;
using SchoolApp.Domain.Contracts;

namespace SchoolApp.Data.Registrations;

public static class DataServiceRegistration
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddTransient<IGenericRepository,GenericRepository>();
        
        return services;
    }
}