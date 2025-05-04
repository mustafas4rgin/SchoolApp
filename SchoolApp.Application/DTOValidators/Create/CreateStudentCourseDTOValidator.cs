using FluentValidation;
using SchoolApp.Application.DTOs;

namespace SchoolApp.Application.DTOValidators.Create;

public class CreateStudentCourseDTOValidator : AbstractValidator<CreateStudentCourseDTO>
{
    public CreateStudentCourseDTOValidator()
    {

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