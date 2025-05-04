using FluentValidation;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Validators;

public class RoleValidator : AbstractValidator<Role>
{
    public RoleValidator()
    {

        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Role name cannot be empty.")
            .Length(2,50)
            .WithMessage("Role must be between 2-50 characters.");
    }
}