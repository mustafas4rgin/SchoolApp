using FluentValidation;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Validators;

public class DepartmentValidator : AbstractValidator<Department>
{
    public DepartmentValidator()
    {
        RuleFor(d => d.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty.")
            .Length(5,75)
            .WithMessage("Title must be between 5-75 characters.");

        RuleFor(d => d.Address)
            .Length(5,500)
            .WithMessage("Address must be between 5-500 characters.");
        
        RuleFor(d => d.FacultyId)
            .NotNull()
            .WithMessage("FacultyId value cannot be null.")
            .GreaterThan(0)
            .WithMessage("FacultyId value must be greater than 0.");
    }
}