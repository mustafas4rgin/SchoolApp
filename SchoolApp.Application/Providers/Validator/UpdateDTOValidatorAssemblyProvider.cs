using SchoolApp.Application.DTOValidators.Update;

namespace SchoolApp.Application.Providers.Validator;

public static class UpdateDTOValidatorAssemblyProvider
{
    public static Type[] GetValidatorAssemblies()
    {
        return new[]
        {
            typeof(UpdateCourseDTOValidator),
            typeof(UpdateGradeDTOValidator),
            typeof(UpdateStudentCourseDTOValidator),
            typeof(UpdateStudentDTOValidator),
            typeof(UpdateTeacherDTOValidator),
            typeof(UpdateRoleDTOValidator),
            typeof(UpdateDepartmentDTOValidator),
            typeof(UpdateFacultyDTOValidator)
        };
    }
}