using FluentValidation;
using SchoolApp.Application.DTOs.Create;

namespace SchoolApp.Application.DTOValidators.Create;

public class CreateSurveyDTOValidator : AbstractValidator<CreateSurveyDTO>
{
    public CreateSurveyDTOValidator()
    {
        RuleFor(s => s.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty.")
            .Length(3,100)
            .WithMessage("Title must be between 3-100 characters.");
    }
}