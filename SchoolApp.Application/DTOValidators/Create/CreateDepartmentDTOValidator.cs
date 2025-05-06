using FluentValidation;
using SchoolApp.Application.DTOs.Create;

namespace SchoolApp.Application.DTOValidators.Create;

public class CreateDepartmentDTOValidator : AbstractValidator<CreateDepartmentDTO>
{
    public CreateDepartmentDTOValidator()
    {
        RuleFor(d => d.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty.")
            .Length(5,75)
            .WithMessage("Title must be between 5-75 characters");

        RuleFor(d => d.Address)
            .Length(5,500)
            .WithMessage("Address must be between 5-500 characters.");
    }
}