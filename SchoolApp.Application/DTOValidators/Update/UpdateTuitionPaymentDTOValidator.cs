namespace SchoolApp.Application.DTOValidators.Update;

using FluentValidation;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

public class UpdateTuitionPaymentDTOValidator : AbstractValidator<UpdateTuitionDTO>
{
    public UpdateTuitionPaymentDTOValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("Öğrenci ID geçerli olmalıdır.");
    }
}
