using SchoolApp.Application.DTOValidators.Create;

namespace SchoolApp.Application.Providers.Validator;

public class CreateDTOValidatorAssemblyProvider
{
    public static Type[] GetValidatorAssemblies()
    {
        return new[]
        {
            typeof(CreateCourseDTOValidator),
            typeof(CreateGradeDTOValidator),
            typeof(CreateStudentCourseDTOValidator),
            typeof(CreateStudentDTOValidator),
            typeof(CreateTeacherDTOValidator),
            typeof(CreateRoleDTOValidator),
            typeof(CreateDepartmentDTOValidator),
            typeof(CreateFacultyDTOValidator),
            typeof(CreateScholarshipApplicationDTOValidator),
            typeof(CreateSurveyQuestionDTOValidator),
            typeof(CreateSurveyAnswerDTOValidator),
            typeof(CreateSurveyOptionDTOValidator),
            typeof(CreateTuitionPaymentDTOValidator),
            typeof(CreateSurveyDTOValidator)
        };
    }
}