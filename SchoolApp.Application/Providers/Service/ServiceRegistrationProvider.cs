using Microsoft.Extensions.DependencyInjection;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.Services;

namespace SchoolApp.Applicaiton.Providers.Service;

public class ServiceRegistrationProvider
{
    public static void RegisterServices(IServiceCollection services)
    {
        var servicesToRegister = new (Type Interface, Type Implementation)[]
        {
            (typeof(IGenericService<>),typeof(GenericService<>)),
            (typeof(IRoleService),typeof(RoleService)),
            (typeof(IStudentService),typeof(StudentService)),
            (typeof(IGradeService),typeof(GradeService)),
            (typeof(ICourseService),typeof(CourseService)),
            (typeof(IStudentCourseService),typeof(StudentCourseService)),
            (typeof(ITeacherService),typeof(TeacherService))
        };
        foreach (var service in servicesToRegister)
        {
            services.AddTransient(service.Interface, service.Implementation);
        }
    }
}