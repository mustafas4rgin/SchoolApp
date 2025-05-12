using FluentValidation;
using SchoolApp.Application.DTOs.Create;

namespace SchoolApp.Application.DTOValidators.Create;

public class CreateSurveyQuestionDTOValidator : AbstractValidator<CreateSurveyQuestionDTO>
{
    public CreateSurveyQuestionDTOValidator()
    {
        RuleFor(sq => sq.QuestionText)
            .NotEmpty()
            .WithMessage("Question text cannot be empty.");
        
        RuleFor(sq => sq.SurveyId)
            .NotNull()
            .WithMessage("SurveyID value cannot be null.")
            .GreaterThan(0)
            .WithMessage("SurveyID value must be greater than zero.");
    }
}