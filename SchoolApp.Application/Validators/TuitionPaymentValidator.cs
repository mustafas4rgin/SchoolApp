using FluentValidation;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Validators;

public class TuitionPaymentValidator : AbstractValidator<TuitionPayment>
{
    public TuitionPaymentValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("Öğrenci ID geçerli olmalıdır.");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0).WithMessage("Toplam ödeme miktarı sıfırdan büyük olmalıdır.");

        RuleFor(x => x.PaidAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Ödenen miktar negatif olamaz.")
            .LessThanOrEqualTo(x => x.TotalAmount)
            .WithMessage("Ödenen miktar toplam miktardan fazla olamaz.");

        RuleFor(x => x.LastPaymentDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Son ödeme tarihi bugünden önce olamaz.");
    }
}