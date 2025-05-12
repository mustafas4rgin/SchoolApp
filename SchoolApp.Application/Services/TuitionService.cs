using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.Helpers;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Enums;
using SchoolApp.Domain.Results;
using SchoolApp.Domain.Results.Raw;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.Application.Services;

public class TuitionService : GenericService<TuitionPayment>, ITuitionService
{
    private readonly ITuitionRepository _tuitionRepository;
    private readonly IValidator<TuitionPayment> _validator;
    public TuitionService(ITuitionRepository tuitionRepository, IValidator<TuitionPayment> validator) : base(tuitionRepository, validator)
    {
        _validator = validator;
        _tuitionRepository = tuitionRepository;
    }
    public async Task<IServiceResult> MockPayment(PaymentParameters param, int studentId)
    {
        try
        {
            if (!Enum.TryParse<TermType>(param.TermType, out var parsedTerm))
                return new ErrorResult("Invalid term value.");

            var payment = await _tuitionRepository.GetAll
                            <TuitionPayment>()
                            .FirstOrDefaultAsync(tp => tp.TermType == parsedTerm
                            && tp.Year == param.Year
                            && tp.StudentId == studentId);

            if (payment is null)
                return new ErrorResult("There is no payment with this information.");

            if (payment.PaidAmount + param.Amount > payment.TotalAmount)
            {
                return new ErrorResult("Payment cannot be greater than total amount.");
            }


            if (payment.PaidAmount >= payment.TotalAmount)
                return new ErrorResult("Payment already completed.");

            _tuitionRepository.Payment(payment, param.Amount);
            await _tuitionRepository.SaveChangesAsync();

            return new SuccessResult("Payment success.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public override async Task<IServiceResult> AddAsync(TuitionPayment payment)
    {
        try
        {
            payment.TermType = (TermType)UniverstiyInformationHelper.GetCurrentTerm();
            payment.TotalAmount = UniverstiyInformationHelper.GetUniversityPrice();
            var existingPayment = await _tuitionRepository
                .GetAll<TuitionPayment>()
                .FirstOrDefaultAsync(tp =>
                    !tp.IsDeleted &&
                    tp.StudentId == payment.StudentId &&
                    tp.Year == payment.Year &&
                    tp.TermType == payment.TermType
                );

            if (existingPayment != null)
            {
                return new ErrorResult("This student has already made a payment for this term.");
            }

            var validationResult = await _validator.ValidateAsync(payment);
            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

            await _tuitionRepository.Add(payment);
            await _tuitionRepository.SaveChangesAsync();

            return new SuccessResult("Payment successfully added.");
        }
        catch (Exception ex)
        {
            return new ErrorResult($"An error occurred while adding payment: {ex.Message}");
        }
    }


    public async Task<IServiceResultWithData<IEnumerable<TuitionPayment>>> GetPaymentsForStudentAsync(int studentId, QueryParameters param)
    {
        try
        {
            var query = _tuitionRepository.GetAll<TuitionPayment>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForTuitionPayment(query, param.Include);

            var payments = await query.Where(tp => !tp.IsDeleted && tp.StudentId == studentId)
                            .ToListAsync();

            if (!payments.Any())
                return new ErrorResultWithData<IEnumerable<TuitionPayment>>("No payment found.");

            return new SuccessResultWithData<IEnumerable<TuitionPayment>>("Payments: ", payments);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<TuitionPayment>>(ex.Message);
        }
    }
}