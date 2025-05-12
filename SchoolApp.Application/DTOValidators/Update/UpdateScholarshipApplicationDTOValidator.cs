using FluentValidation;
using SchoolApp.Application.DTOs.Update;

namespace SchoolApp.Application.DTOValidators.Update;

public class UpdateScholarshipDTOValidator : AbstractValidator<UpdateScholarshipDTO>
{
    public UpdateScholarshipDTOValidator()
    {
        RuleFor(sa => sa.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .EmailAddress()
            .WithMessage("Must be a valid email address.")
            .Length(3,100)
            .WithMessage("Email must be between 3-100 characters.");

        RuleFor(sa => sa.FullName)
            .NotEmpty()
            .WithMessage("Name cannot be empty.")
            .Length(3,100)
            .WithMessage("Name must be between 3-100 characters.");

        RuleFor(sa => sa.Note)
            .NotNull()
            .WithMessage("Note cannot be null.");

        RuleFor(sa => sa.Phone)
            .NotEmpty()
            .WithMessage("Phone is required.");
        
        RuleFor(sa => sa.IncomeStatus)
            .NotEmpty()
            .WithMessage("Income status cannot be empty.")
            .Length(1,15)
            .WithMessage("Income status must be between 1-15 characters.");

        RuleFor(sa => sa.StudentId)
            .NotNull()
            .WithMessage("Student ID value cannot be null.")
            .GreaterThan(0)
            .WithMessage("Student ID value must be greater than zero.");

        RuleFor(sa => sa.StudentNumber)
            .NotEmpty()
            .WithMessage("Student number cannot be empty.");
    }
}