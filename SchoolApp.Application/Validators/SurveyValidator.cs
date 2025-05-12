using FluentValidation;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Validators;

public class SurveyValidator : AbstractValidator<Survey>
{
    public SurveyValidator()
    {
        RuleFor(s => s.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty.")
            .Length(3,100)
            .WithMessage("Title must be between 3-100 characters.");
    }
}