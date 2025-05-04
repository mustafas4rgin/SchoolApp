using FluentValidation;
using SchoolApp.Application.DTOs;

namespace SchoolApp.Application.DTOValidators.Update;

public class UpdateTeacherDTOValidator : AbstractValidator<UpdateTeacherDTO>
{
    public UpdateTeacherDTOValidator()
    {
        RuleFor(s => s.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .Length(3,100)
            .WithMessage("Email must be between 3-100 characters.");

        RuleFor(s => s.FirstName)
            .NotEmpty()
            .WithMessage("First name cannot be empty.")
            .Length(3,50)
            .WithMessage("First name must be between 3-50 characters.");

        RuleFor(s => s.LastName)
            .NotEmpty()
            .WithMessage("Last name cannot be empty.")
            .Length(3,50)
            .WithMessage("Last name must be between 3-50 characters.");

        RuleFor(s => s.Phone)
            .Length(3,15)
            .WithMessage("Phone must be between 3-15 characters.");

        RuleFor(s => s.Number)
            .NotNull()
            .WithMessage("Student number cannot be null.")
            .Length(10)
            .WithMessage("Number must be exact 10 characters.");
        
        RuleFor(s => s.RoleId)
            .NotNull()
            .WithMessage("Role ID cannot be null.")
            .GreaterThan(0)
            .WithMessage("Role ID must be greater than zero.");
    }
}