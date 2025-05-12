using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;

namespace SchoolApp.Application.Concrete;

public interface ITuitionService : IGenericService<TuitionPayment>
{
    Task<IServiceResultWithData<IEnumerable<TuitionPayment>>> GetPaymentsForStudentAsync(int studentId, QueryParameters param);
    Task<IServiceResult> MockPayment(PaymentParameters param,int studentId);
}