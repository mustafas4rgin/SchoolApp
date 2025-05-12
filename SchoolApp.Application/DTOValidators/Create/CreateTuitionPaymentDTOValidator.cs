using FluentValidation;
using SchoolApp.Application.DTOs.Create;

namespace SchoolApp.Application.DTOValidators.Create;

public class CreateTuitionPaymentDTOValidator : AbstractValidator<CreateTuitionPaymentDTO>
{
    public CreateTuitionPaymentDTOValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("Öğrenci ID geçerli olmalıdır.");
    }
}
