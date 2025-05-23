using FluentValidation;
using SchoolApp.Application.DTOs.Update;

namespace SchoolApp.Application.DTOValidators.Update;

public class UpdateCourseDTOValidator : AbstractValidator<UpdateCourseDTO>
{
    public UpdateCourseDTOValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Course name cannot be empty.")
            .Length(2,70)
            .WithMessage("Course name must be between 2-70 characters.");

        RuleFor(c => c.Credit)
            .NotNull()
            .WithMessage("Credit cannot be null.")
            .GreaterThan(0)
            .WithMessage("Creadit must be greater than zero.");

        RuleFor(c => c.Quota)
            .GreaterThan(0)
            .WithMessage("Quota must be greater than zero.");

        RuleFor(c => c.Year)
            .NotEmpty()
            .WithMessage("Course year cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Course year must be greater than zero.")
            .LessThanOrEqualTo(4)
            .WithMessage("Course year must be less than or equal to 4.");

        RuleFor(c => c.TeacherId)
            .NotNull()
            .WithMessage("TeacherID value cannot be null.")
            .GreaterThan(0)
            .WithMessage("TeacherID value must be greater than zero.");
    }
}