using FluentValidation;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Validators;

public class SurveyOptionValidator : AbstractValidator<SurveyOption>
{
    public SurveyOptionValidator()
    {
        RuleFor(so => so.Text)
            .NotEmpty()
            .WithMessage("Text cannot be empty.");

        RuleFor(so => so.QuestionId)
            .NotNull()
            .WithMessage("QuestionID value cannot be null.")
            .GreaterThan(0)
            .WithMessage("QuestionID value must be greater than zero.");
    }
}