using FluentValidation;
using SchoolApp.Application.DTOs;

namespace SchoolApp.Application.DTOValidators.Create;

public class CreateStudentDTOValidator : AbstractValidator<CreateStudentDTO>
{
    public CreateStudentDTOValidator()
    {
        RuleFor(s => s.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .EmailAddress()
            .WithMessage("Must be a valid email address.")
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

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is necessary.")
            .MinimumLength(8).WithMessage("Password must be atleast 8 characters.")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one upper case letter.")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lower case letter.")
            .Matches(@"\d+").WithMessage("Password must contain at least one number.")
            .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=.]+").WithMessage("Password must contain at least one speacial characters.");
    }
}