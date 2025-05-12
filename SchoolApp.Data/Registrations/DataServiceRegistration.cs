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
        services.AddTransient<ITeacherRepository, TeacherRepository>();
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<IStudentCourseRepository, StudentCourseRepository>();
        services.AddTransient<ICourseRepository, CourseRepository>();
        services.AddTransient<ITuitionRepository, TuitionRepository>();
        services.AddTransient<ISurveyRepository, SurveyRepository>();
        services.AddTransient<ISurveyQuestionRepository, SurveyQuestionRepository>();
        services.AddTransient<ISurveyStudentRepository, SurveyStudentRepository>();

        return services;
    }
}