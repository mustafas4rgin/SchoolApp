using FluentValidation;
using SchoolApp.Application.DTOs.Update;

namespace SchoolApp.Application.DTOValidators.Update;

public class UpdateSurveyDTOValidator : AbstractValidator<UpdateSurveyDTO>
{
    public UpdateSurveyDTOValidator()
    {
        RuleFor(s => s.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty.")
            .Length(3,100)
            .WithMessage("Title must be between 3-100 characters.");

    }
}