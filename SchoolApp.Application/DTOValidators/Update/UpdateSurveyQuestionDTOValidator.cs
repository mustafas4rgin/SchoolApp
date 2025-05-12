using FluentValidation;
using SchoolApp.Application.DTOs.Update;

namespace SchoolApp.Application.DTOValidators.Update;

public class UpdateSurveyQuestionDTOValidator : AbstractValidator<UpdateSurveyQuestionDTO>
{
    public UpdateSurveyQuestionDTOValidator()
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