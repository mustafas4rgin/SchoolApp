using FluentValidation;
using SchoolApp.Application.DTOs.Update;

namespace SchoolApp.Application.DTOValidators;

public class UpdateSurveyOptionDTOValidator : AbstractValidator<UpdateSurveyOptionDTO>
{
    public UpdateSurveyOptionDTOValidator()
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