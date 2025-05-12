using FluentValidation;
using SchoolApp.Application.DTOs.Create;

namespace SchoolApp.Application.DTOValidators.Create;

public class CreateSurveyAnswerDTOValidator : AbstractValidator<CreateSurveyAnswerDTO>
{
    public CreateSurveyAnswerDTOValidator()
    {
        RuleFor(sa => sa.QuestionId)
            .NotNull()
            .WithMessage("Question ID value cannot be null.")
            .GreaterThan(0)
            .WithMessage("Question ID value must be greater than zero.");

        RuleFor(sa => sa.SelectedOptionId)
            .NotNull()
            .WithMessage("SelectedOptionID value cannot be null.")
            .GreaterThan(0)
            .WithMessage("SelectedOptionID value must be greater than zero.");

        RuleFor(sa => sa.StudentId)
            .NotNull()
            .WithMessage("StudentID value cannot be null.")
            .GreaterThan(0)
            .WithMessage("StudentID value must be greater than zero.");
    }
}