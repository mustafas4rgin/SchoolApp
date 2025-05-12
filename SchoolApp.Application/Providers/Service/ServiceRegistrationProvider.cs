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
            (typeof(ITeacherService),typeof(TeacherService)),
            (typeof(IFacultyService),typeof(FacultyService)),
            (typeof(IDepartmentService),typeof(DepartmentService)),
            (typeof(IAuthService),typeof(AuthService)),
            (typeof(ITokenService),typeof(TokenService)),
            (typeof(ISurveyQuestionService),typeof(SurveyQuestionService)),
            (typeof(ITuitionService),typeof(TuitionService)),
            (typeof(ISurveyService),typeof(SurveyService)),
            (typeof(ISurveyAnswerService),typeof(SurveyAnswerService)),
            (typeof(ISurveyStudentService),typeof(SurveyStudentService))
        };
        foreach (var service in servicesToRegister)
        {
            services.AddTransient(service.Interface, service.Implementation);
        }
    }
}