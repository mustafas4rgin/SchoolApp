using FluentValidation;
using SchoolApp.Application.DTOs.Update;

namespace SchoolApp.Application.DTOValidators.Update;

public class UpdateStudentCourseDTOValidator : AbstractValidator<UpdateStudentCourseDTO>
{
    public UpdateStudentCourseDTOValidator()
    {
        RuleFor(sc => sc.Attendance)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Attendance must be greater than or equal to 0.");

        RuleFor(sc => sc.CourseId)
            .NotNull()
            .WithMessage("Course ID value cannot be null.")
            .GreaterThan(0)
            .WithMessage("Course ID value must be greater than zero.");

        RuleFor(sc => sc.StudentId)
            .NotNull()
            .WithMessage("Student ID value cannot be null.")
            .GreaterThan(0)
            .WithMessage("Student ID value must be graeter than zero.");
    }
}