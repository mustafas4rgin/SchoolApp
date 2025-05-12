using FluentValidation;
using SchoolApp.Application.DTOs;

namespace SchoolApp.Application.DTOValidators.Create;

public class CreateSurveyOptionDTOValidator : AbstractValidator<CreateSurveyOptionDTO>
{
    public CreateSurveyOptionDTOValidator()
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