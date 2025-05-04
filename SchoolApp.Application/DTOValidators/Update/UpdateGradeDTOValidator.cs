using FluentValidation;
using SchoolApp.Application.DTOs.Update;

namespace SchoolApp.Application.DTOValidators.Update;

public class UpdateGradeDTOValidator : AbstractValidator<UpdateGradeDTO>
{
    public UpdateGradeDTOValidator()
    {
         RuleFor(g => g.CourseId)
            .NotNull()
            .WithMessage("Course ID value cannot be null.")
            .GreaterThan(0)
            .WithMessage("Course ID value must be greater than zero.");

        RuleFor(g => g.StudentId)
            .NotNull()
            .WithMessage("Student ID value cannot be null.")
            .GreaterThan(0)
            .WithMessage("Student ID value must be greater than zero.");

        RuleFor(g => g.Note)
            .NotNull()
            .WithMessage("Note cannot be null.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Note must be greater than or equal to zero.")
            .LessThanOrEqualTo(100)
            .WithMessage("Note must be less than or equal to 100.");
    }
}