using SchoolApp.Domain.Entities;

namespace SchoolApp.Domain.Contracts;

public interface ITuitionRepository : IGenericRepository
{
    void Payment(TuitionPayment payment, int amount);

}