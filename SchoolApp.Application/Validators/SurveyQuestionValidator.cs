using FluentValidation;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Validators;

public class SurveyQuestionValidator : AbstractValidator<SurveyQuestion>
{
    public SurveyQuestionValidator()
    {
        RuleFor(sq => sq.QuestionText)
            .NotEmpty()
            .WithMessage("Question text cannot be empty.");
    }
}