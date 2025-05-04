using FluentValidation;
using SchoolApp.Application.DTOs.Update;

namespace SchoolApp.Application.DTOValidators.Update;

public class UpdateRoleDTOValidator : AbstractValidator<UpdateRoleDTO>
{
    public UpdateRoleDTOValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Role name cannot be empty.")
            .Length(2,50)
            .WithMessage("Role must be between 2-50 characters.");
    }
}