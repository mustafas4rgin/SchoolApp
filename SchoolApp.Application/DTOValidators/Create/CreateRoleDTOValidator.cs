using FluentValidation;
using SchoolApp.Application.DTOs.Create;

namespace SchoolApp.Application.DTOValidators.Create;

public class CreateRoleDTOValidator : AbstractValidator<CreateRoleDTO>
{
    public CreateRoleDTOValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Role name cannot be empty.")
            .Length(2,50)
            .WithMessage("Role must be between 2-50 characters.");
    }
}