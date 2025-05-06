using FluentValidation;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Validators;

public class FacultyValidator : AbstractValidator<Faculty>
{
    public FacultyValidator()
    {
        RuleFor(f => f.Address)
            .Length(5,500)
            .WithMessage("Address must be between 5-500 characters.");

        RuleFor(f => f.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty.")
            .Length(5,75)
            .WithMessage("Title must be between 5-75 characters.");
    }
}