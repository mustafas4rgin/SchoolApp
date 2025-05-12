using SchoolApp.Application.Validators;

namespace SchoolApp.Application.Providers.Validator;

public static class EntityValidatorAssemblyProvider
{
    public static Type[] GetValidatorAssemblies()
    {
        return new[]
        {
            typeof(CourseValidator),
            typeof(GradeValidator),
            typeof(StudentCourseValidator),
            typeof(StudentValidator),
            typeof(TeacherValidator),
            typeof(RoleValidator),
            typeof(FacultyValidator),
            typeof(DepartmentValidator),
            typeof(ScholarshipApplicationValidator),
            typeof(SurveyQuestionValidator),
            typeof(SurveyAnswerValidator),
            typeof(SurveyOptionValidator),
            typeof(TuitionPaymentValidator),
            typeof(SurveyValidator)
        };
    }
}